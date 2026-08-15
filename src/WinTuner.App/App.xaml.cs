using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace WinTuner.App;

/// <summary>Application entry point for the WinTuner WinUI 3 shell.</summary>
public partial class App : Application
{
    /// <summary>The main window, exposed so it can be referenced after launch.</summary>
    public static Window? MainWindow { get; private set; }

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow();
        MainWindow.Activate();
    }
}
