using System.Collections.Generic;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinTuner.Core.Registry;
using WinTuner.Core.Tweaks;

namespace WinTuner.App;

/// <summary>
/// The main window. Presents the tweak catalog grouped by category and lets the
/// user inspect, apply, revert, or reset each tweak via the real registry engine.
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly TweakEngine _engine = new(new RealRegistryProvider());
    private IReadOnlyList<RegistryTweak> _visible = Catalog.All;
    private RegistryTweak? _selected;

    public MainWindow()
    {
        this.InitializeComponent();
        this.Title = "WinTuner - Windows Tweak Studio";

        CategoryList.ItemsSource = System.Enum.GetNames<TweakCategory>();
        CategoryList.SelectedIndex = 0;
    }

    private void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (CategoryList.SelectedItem is not string name)
        {
            return;
        }

        if (!System.Enum.TryParse<TweakCategory>(name, out var cat))
        {
            return;
        }

        _visible = Catalog.All.Where(t => t.Category == cat).ToList();
        TweakList.ItemsSource = _visible;
        _selected = null;
        ClearDetail();
    }

    private void OnTweakSelected(object sender, SelectionChangedEventArgs e)
    {
        _selected = TweakList.SelectedItem as RegistryTweak;
        if (_selected is null)
        {
            ClearDetail();
            return;
        }

        TitleText.Text = _selected.Title;
        DescText.Text = _selected.Description;
        RefText.Text = _selected.Reference ?? string.Empty;
        RefreshState();
    }

    private void OnApply(object sender, RoutedEventArgs e)
    {
        if (_selected is null)
        {
            return;
        }

        _engine.Apply(_selected);
        StatusText.Text = $"Applied '{_selected.Title}'.";
        RefreshState();
    }

    private void OnRevert(object sender, RoutedEventArgs e)
    {
        if (_selected is null)
        {
            return;
        }

        _engine.Revert(_selected);
        StatusText.Text = $"Reverted '{_selected.Title}'.";
        RefreshState();
    }

    private void OnReset(object sender, RoutedEventArgs e)
    {
        if (_selected is null)
        {
            return;
        }

        _engine.Reset(_selected);
        StatusText.Text = $"Reset '{_selected.Title}' to default.";
        RefreshState();
    }

    private void RefreshState()
    {
        if (_selected is null)
        {
            return;
        }

        var elevation = _selected.RequiresElevation ? "  (requires administrator)" : string.Empty;
        StateText.Text = $"State: {_engine.GetState(_selected)}{elevation}";
    }

    private void ClearDetail()
    {
        TitleText.Text = string.Empty;
        DescText.Text = string.Empty;
        RefText.Text = string.Empty;
        StateText.Text = string.Empty;
        StatusText.Text = string.Empty;
    }
}
