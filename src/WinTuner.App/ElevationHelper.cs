using System.Diagnostics;
using System.Security.Principal;

namespace WinTuner.App;

/// <summary>
/// Helpers for detecting and acquiring administrator rights. Several WinTuner
/// tweaks write to HKLM, which fails unless the process is elevated; this lets
/// the app offer a one-click relaunch as administrator.
/// </summary>
public static class ElevationHelper
{
    /// <summary>True when the current process is running with an administrator token.</summary>
    public static bool IsElevated()
    {
        using var identity = WindowsIdentity.GetCurrent();
        return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>Relaunches the current executable elevated (UAC prompt) and exits this instance.</summary>
    public static void RelaunchAsAdmin()
    {
        var exe = Process.GetCurrentProcess().MainModule?.FileName
                  ?? throw new System.InvalidOperationException("Unable to determine the running executable path.");

        var start = new ProcessStartInfo(exe) { Verb = "runas", UseShellExecute = true };
        Process.Start(start);
        System.Environment.Exit(0);
    }
}
