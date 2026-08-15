using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.Graphics;
using WinTuner.Core.Registry;
using WinTuner.Core.Tweaks;

namespace WinTuner.App;

/// <summary>
/// The main window. Presents the tweak catalog through a WinUI 3 NavigationView
/// (left rail, Fluent icons) with each tweak rendered as a card: a ToggleSwitch to
/// apply/revert, a live state read-out, the citable registry reference, and a
/// Reset-to-default action. Status is surfaced via an InfoBar.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly TweakEngine _engine = new(new RealRegistryProvider());
    private readonly Dictionary<TweakCategory, IReadOnlyList<TweakViewModel>> _byCategory = new();

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

        if (_byCategory.Count > 0)
        {
            var first = _byCategory.Keys.First();
            CategoryTitle.Text = CategoryLabel(first);
            TweakList.ItemsSource = _byCategory[first];
            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
        }
    }

    private void OnNavSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is not NavigationViewItem item || item.Tag is not TweakCategory cat)
        {
            return;
        }

        CategoryTitle.Text = CategoryLabel(cat);
        TweakList.ItemsSource = _byCategory[cat];
    }

    private void OnToggle(object sender, RoutedEventArgs e)
    {
        if (sender is not ToggleSwitch toggle || toggle.DataContext is not TweakViewModel vm)
        {
            return;
        }

        if (toggle.IsOn)
        {
            vm.Apply();
        }
        else
        {
            vm.Revert();
        }

        vm.Refresh();
        ShowStatus($"{(toggle.IsOn ? "Applied" : "Reverted")}: {vm.Title}", InfoBarSeverity.Success);
    }

    private void OnReset(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement fe || fe.DataContext is not TweakViewModel vm)
        {
            return;
        }

        vm.Reset();
        vm.Refresh();
        ShowStatus($"Reset to default: {vm.Title}", InfoBarSeverity.Informational);
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
        TweakCategory.System => "\uE713",       // Settings (gear)
        TweakCategory.Performance => "\uE9D9",  // Performance
        TweakCategory.Appearance => "\uE790",   // Brush
        TweakCategory.Security => "\uE7EF",     // Shield
        _ => "\uE8C0",                          // Globe (Network / fallback)
    };
}
