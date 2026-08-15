using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Graphics;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinTuner.Core.Profile;
using WinTuner.Core.Registry;
using WinTuner.Core.Tweaks;

namespace WinTuner.App;

/// <summary>
/// The main window. Presents the tweak catalog through a WinUI 3 NavigationView
/// (left rail, Fluent icons) with each tweak rendered as a card: a ToggleSwitch to
/// apply/revert, a live state read-out, the citable registry reference, and a
/// Reset-to-default action. Category-wide Apply all / Reset all, an admin
/// relaunch affordance for HKLM tweaks, and profile Export/Import are provided.
/// Status is surfaced via an InfoBar.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly TweakEngine _engine = new(new RealRegistryProvider());
    private readonly Dictionary<TweakCategory, IReadOnlyList<TweakViewModel>> _byCategory = new();
    private TweakCategory _currentCategory;
    private const string ScanViewTag = "Scan";
    private object? _categoryView;
    private bool _onScanView;

    public MainWindow()
    {
        this.InitializeComponent();

        // Modern window chrome: Mica backdrop + a sensible default size, centered.
        this.AppWindow.Title = "WinTuner";
        this.SystemBackdrop = new MicaBackdrop
        {
            Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base,
        };

        const int width = 1120;
        const int height = 760;
        var area = DisplayArea.Primary;
        this.AppWindow.Resize(new SizeInt32(width, height));
        this.AppWindow.Move(new PointInt32(
            (area.WorkArea.Width - width) / 2,
            (area.WorkArea.Height - height) / 2));

        // Build the nav rail from categories that actually contain tweaks, so empty
        // (not-yet-implemented) categories never appear in the UI.
        foreach (var cat in Catalog.All.Select(t => t.Category).Distinct())
        {
            _byCategory[cat] = Catalog.All
                .Where(t => t.Category == cat)
                .Select(t => new TweakViewModel(t, _engine))
                .ToList();

            NavView.MenuItems.Add(new NavigationViewItem
            {
                Content = CategoryLabel(cat),
                Icon = new FontIcon { Glyph = CategoryGlyph(cat) },
                Tag = cat,
            });
        }

        // A dedicated System Scan entry (not a tweak category) that reports the
        // live state of every tweak across the whole catalog at once.
        NavView.MenuItems.Add(new NavigationViewItem
        {
            Content = "System Scan",
            Icon = new FontIcon { Glyph = "\uE71E" }, // Scan
            Tag = ScanViewTag,
        });

        if (_byCategory.Count > 0)
        {
            var first = _byCategory.Keys.First();
            ShowCategory(first);
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
        }

        // Show the admin affordances only when elevation is actually required and
        // we are not already running elevated.
        bool needsAdmin = !ElevationHelper.IsElevated() && Catalog.All.Any(t => t.RequiresElevation);
        AdminBanner.IsOpen = needsAdmin;
        AdminButton.Visibility = needsAdmin ? Visibility.Visible : Visibility.Collapsed;
    }

    private void ShowCategory(TweakCategory cat)
    {
        _currentCategory = cat;
        _onScanView = false;
        CategoryTitle.Text = CategoryLabel(cat);
        SubtitleText.Text = "Toggle to apply or revert. Every change writes a citable registry value you can reset anytime.";
        ApplyAllButton.Visibility = Visibility.Visible;
        ResetAllButton.Visibility = Visibility.Visible;
        RefreshButton.Visibility = Visibility.Collapsed;
        AdminBanner.Visibility = Visibility.Visible;

        // Restore the default category view (captured once from the XAML tree).
        if (_categoryView is null)
        {
            _categoryView = ContentArea.Content;
        }
        ContentArea.Content = _categoryView;
        TweakList.ItemsSource = _byCategory[cat];
    }

    private void OnNavSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item)
        {
            return;
        }

        if (item.Tag as string == ScanViewTag)
        {
            ShowScan();
            return;
        }

        if (item.Tag is TweakCategory cat)
        {
            ShowCategory(cat);
        }
    }

    private void OnToggle(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggle || toggle.DataContext is not TweakViewModel vm)
        {
            return;
        }

        bool desired = toggle.IsOn;
        try
        {
            if (desired)
            {
                vm.Apply();
            }
            else
            {
                vm.Revert();
            }

            vm.Refresh();
            ShowStatus($"{(desired ? "Applied" : "Reverted")}: {vm.Title}", InfoBarSeverity.Success);
        }
        catch (Exception ex)
        {
            // The registry write failed - commonly because the tweak writes to
            // HKLM and the app is not elevated. Re-sync the toggle to the real
            // state so the UI never lies about what is actually applied.
            vm.Refresh();
            ShowStatus($"Could not apply '{vm.Title}': {Friendly(ex)}", InfoBarSeverity.Error);
            AdminBanner.IsOpen = true;
        }
    }

    private void OnReset(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe || fe.DataContext is not TweakViewModel vm)
        {
            return;
        }

        try
        {
            vm.Reset();
            vm.Refresh();
            ShowStatus($"Reset to default: {vm.Title}", InfoBarSeverity.Informational);
        }
        catch (Exception ex)
        {
            vm.Refresh();
            ShowStatus($"Could not reset '{vm.Title}': {Friendly(ex)}", InfoBarSeverity.Error);
        }
    }

    private void OnApplyAll(object sender, RoutedEventArgs e)
    {
        int ok = 0, fail = 0;
        foreach (var vm in _byCategory[_currentCategory])
        {
            try
            {
                vm.Apply();
                vm.Refresh();
                ok++;
            }
            catch (Exception)
            {
                fail++;
            }
        }

        ShowStatus(
            fail == 0
                ? $"Applied all {ok} tweaks in {CategoryLabel(_currentCategory)}."
                : $"Applied {ok}, skipped {fail} (needs administrator) in {CategoryLabel(_currentCategory)}.",
            fail == 0 ? InfoBarSeverity.Success : InfoBarSeverity.Warning);
    }

    private void ShowScan()
    {
        _onScanView = true;
        CategoryTitle.Text = "System Scan";
        SubtitleText.Text = "Live state of every tweak, read directly from your registry. Apply or revert any item inline.";
        ApplyAllButton.Visibility = Visibility.Collapsed;
        ResetAllButton.Visibility = Visibility.Collapsed;
        RefreshButton.Visibility = Visibility.Visible;
        AdminBanner.Visibility = Visibility.Collapsed;

        ShowScanImpl();
    }

    private void ShowScanImpl()
    {
        var rows = Catalog.All
            .Select(t => new ScanRowViewModel(t, _engine))
            .OrderBy(r => r.CategoryLabel)
            .ThenBy(r => r.Title)
            .ToList();

        int enabled = rows.Count(r => r.State == TweakState.Enabled);
        int disabled = rows.Count(r => r.State == TweakState.Disabled);
        int unknown = rows.Count(r => r.State == TweakState.Unknown);

        var summary = new TextBlock
        {
            Text = $"{Catalog.All.Count} tweaks scanned · {enabled} enabled · {disabled} disabled · {unknown} not at a known setting",
            FontSize = 15,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            Margin = new Thickness(0, 0, 0, 14),
            TextWrapping = TextWrapping.Wrap,
        };

        var panel = new StackPanel { Spacing = 10 };
        panel.Children.Add(summary);
        foreach (var row in rows)
        {
            panel.Children.Add(BuildScanRow(row));
        }

        var scroll = new ScrollViewer { VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
        scroll.Content = panel;
        ContentArea.Content = scroll;
    }

    private UIElement BuildScanRow(ScanRowViewModel row)
    {
        var border = new Border
        {
            Background = (Brush)Application.Current.Resources["CardBackgroundFillColorDefaultBrush"],
            BorderBrush = (Brush)Application.Current.Resources["CardStrokeColorDefaultBrush"],
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(10),
            Padding = new Thickness(18),
        };

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
                new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) },
            },
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
                new RowDefinition { Height = new GridLength(0, GridUnitType.Auto) },
            },
        };

        var title = new TextBlock
        {
            Text = row.Title,
            FontSize = 18,
            FontWeight = Microsoft.UI.Text.FontWeights.SemiBold,
            VerticalAlignment = VerticalAlignment.Center,
        };

        var stateChip = new TextBlock
        {
            Text = row.StateText,
            FontSize = 12,
            Opacity = 0.75,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 6, 0, 8),
        };

        var toggle = new ToggleSwitch
        {
            IsOn = row.IsOn,
            VerticalAlignment = VerticalAlignment.Center,
        };
        toggle.Toggled += (sender, _) =>
        {
            if (sender is not ToggleSwitch t)
            {
                return;
            }

            try
            {
                if (t.IsOn)
                {
                    row.Apply();
                }
                else
                {
                    row.Revert();
                }

                row.Refresh();
                t.IsOn = row.IsOn;
                ShowStatus($"{(t.IsOn ? "Applied" : "Reverted")}: {row.Title}", InfoBarSeverity.Success);
                // Refresh the per-row state chip without rebuilding the whole list.
                stateChip.Text = row.StateText;
            }
            catch (Exception ex)
            {
                row.Refresh();
                t.IsOn = row.IsOn;
                ShowStatus($"Could not apply '{row.Title}': {Friendly(ex)}", InfoBarSeverity.Error);
            }
        };

        var detail = new TextBlock
        {
            Text = $"{row.CategoryLabel} · {row.Reference}",
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12,
            Opacity = 0.7,
            TextWrapping = TextWrapping.Wrap,
            VerticalAlignment = VerticalAlignment.Center,
        };

        grid.Children.Add(title);
        Grid.SetRow(title, 0);
        Grid.SetColumn(title, 0);
        Grid.SetColumnSpan(title, 2);

        grid.Children.Add(toggle);
        Grid.SetRow(toggle, 0);
        Grid.SetColumn(toggle, 2);

        grid.Children.Add(stateChip);
        Grid.SetRow(stateChip, 1);
        Grid.SetColumn(stateChip, 0);
        Grid.SetColumnSpan(stateChip, 2);

        grid.Children.Add(detail);
        Grid.SetRow(detail, 2);
        Grid.SetColumn(detail, 0);
        Grid.SetColumnSpan(detail, 2);

        border.Child = grid;
        return border;
    }

    private void OnRefresh(object sender, RoutedEventArgs e)
    {
        if (!_onScanView)
        {
            return;
        }

        // Re-sync category cards if the user is on a category...
        foreach (var list in _byCategory.Values)
        {
            foreach (var vm in list)
            {
                vm.Refresh();
            }
        }

        // ...and rebuild the scan list from the live registry.
        ShowScanImpl();
        ShowStatus("Scanned your system registry for all tweaks.", InfoBarSeverity.Informational);
    }

    private void OnResetAll(object sender, RoutedEventArgs e)
    {
        int ok = 0, fail = 0;
        foreach (var vm in _byCategory[_currentCategory])
        {
            try
            {
                vm.Reset();
                vm.Refresh();
                ok++;
            }
            catch (Exception)
            {
                fail++;
            }
        }

        ShowStatus(
            fail == 0
                ? $"Reset {ok} tweaks in {CategoryLabel(_currentCategory)} to default."
                : $"Reset {ok}, skipped {fail} in {CategoryLabel(_currentCategory)}.",
            fail == 0 ? InfoBarSeverity.Informational : InfoBarSeverity.Warning);
    }

    private void OnRelaunchAdmin(object sender, RoutedEventArgs e) => ElevationHelper.RelaunchAsAdmin();

    private async void OnExport(object sender, RoutedEventArgs e)
    {
        var picker = new FileSavePicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            SuggestedFileName = "wintuner-profile",
        };
        picker.FileTypeChoices.Add("WinTuner profile", new[] { ".json" });

        // WinUI file pickers require a window handle on Win32.
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        var file = await picker.PickSaveFileAsync();
        if (file is null)
        {
            return;
        }

        var json = WinTuner.Core.Profile.ProfileService.Export(Catalog.All, _engine);
        await File.WriteAllTextAsync(file.Path, json);
        ShowStatus($"Exported {Catalog.All.Count} tweak states to {file.Name}.", InfoBarSeverity.Success);
    }

    private async void OnImport(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
        };
        picker.FileTypeFilter.Add(".json");

        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        var file = await picker.PickSingleFileAsync();
        if (file is null)
        {
            return;
        }

        string json;
        try
        {
            json = await File.ReadAllTextAsync(file.Path);
        }
        catch (Exception ex)
        {
            ShowStatus($"Could not read profile: {ex.Message}", InfoBarSeverity.Error);
            return;
        }

        var states = WinTuner.Core.Profile.ProfileService.Parse(json);
        if (states.Count == 0)
        {
            ShowStatus("That file is not a valid WinTuner profile.", InfoBarSeverity.Error);
            return;
        }

        int applied = 0, failed = 0;
        foreach (var tweak in Catalog.All)
        {
            try
            {
                _engine.ApplyOrRevert(tweak, states);
                applied++;
            }
            catch (Exception)
            {
                failed++;
            }
        }

        // Re-sync every card from the registry so the UI reflects the new state.
        foreach (var list in _byCategory.Values)
        {
            foreach (var vm in list)
            {
                vm.Refresh();
            }
        }

        ShowStatus(
            failed == 0
                ? $"Imported profile: {applied} tweaks set."
                : $"Imported profile: {applied} set, {failed} skipped (needs administrator).",
            failed == 0 ? InfoBarSeverity.Success : InfoBarSeverity.Warning);
    }

    private static string Friendly(Exception ex)
    {
        string msg = ex.Message;
        if (msg.Contains("denied", StringComparison.OrdinalIgnoreCase) ||
            msg.Contains("Unauthorized", StringComparison.OrdinalIgnoreCase))
        {
            return "this writes to HKLM and requires administrator. Relaunch WinTuner as admin.";
        }

        return msg;
    }

    private void ShowStatus(string message, InfoBarSeverity severity)
    {
        StatusBar.Title = message;
        StatusBar.Severity = severity;
        StatusBar.IsOpen = true;
    }

    private static string CategoryLabel(TweakCategory cat) => cat switch
    {
        TweakCategory.Explorer => "File Explorer",
        TweakCategory.Privacy => "Privacy",
        TweakCategory.Performance => "Performance",
        TweakCategory.Appearance => "Appearance",
        TweakCategory.System => "System",
        TweakCategory.Security => "Security",
        TweakCategory.Network => "Network",
        TweakCategory.Gaming => "Gaming",
        _ => cat.ToString(),
    };

    private static string CategoryGlyph(TweakCategory cat) => cat switch
    {
        TweakCategory.Explorer => "\uE8B7",     // Folder
        TweakCategory.Privacy => "\uE72E",      // Lock
        TweakCategory.Performance => "\uE9D9",  // Performance
        TweakCategory.Appearance => "\uE790",   // Brush
        TweakCategory.System => "\uE713",       // Settings (gear)
        TweakCategory.Security => "\uE7EF",     // Shield
        TweakCategory.Network => "\uE8C0",      // Globe
        TweakCategory.Gaming => "\uE99D",       // Xbox
        _ => "\uE8C0",                          // Globe (fallback)
    };
}
