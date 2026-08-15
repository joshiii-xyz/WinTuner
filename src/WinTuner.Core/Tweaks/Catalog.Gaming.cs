using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Gaming category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetGaming() => new List<RegistryTweak>
    {
        new()
        {
        Id = "gaming.enable-game-mode",
        Title = "Enable Game Mode",
        Description = "Turns on Game Mode (AutoGameModeEnabled=1), which prioritizes CPU/GPU for the active " +
        "game and suppresses background activity during play. Windows 11 ships with this on by " +
        "default.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\GameBar",
        ValueName = "AutoGameModeEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\GameBar!AutoGameModeEnabled (1 = on)",
        },
        new()
        {
        Id = "gaming.enable-gpu-scheduling",
        Title = "Enable hardware-accelerated GPU scheduling",
        Description = "Offloads some GPU scheduling work to the GPU's hardware scheduler (HwSchMode=2), which " +
        "can lower latency and improve frame consistency on supported GPUs/drivers. REQUIRES " +
        "ELEVATION and a reboot. Not all hardware benefits; revert if you see regressions.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers",
        ValueName = "HwSchMode",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 2,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\GraphicsDrivers!HwSchMode (2 = on, 1 = off)",
        },
        new()
        {
        Id = "gaming.disable-gamedvr",
        Title = "Disable Xbox Game Bar / Game DVR",
        Description = "Fully disables the Xbox Game Bar and DVR via policy (AllowGameDVR=0). Useful on systems " +
        "where it interferes with games, overlays, or screen capture. Requires elevation; Reset " +
        "deletes the policy to restore the default (allowed).",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\GameDVR",
        ValueName = "AllowGameDVR",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\GameDVR!AllowGameDVR (0 = off, policy)",
        },
        new()
        {
        Id = "gaming.disable-mouse-acceleration",
        Title = "Disable mouse acceleration (Enhance pointer precision)",
        Description = "Turns off pointer acceleration so the cursor moves a consistent distance per physical " +
        "movement, which many gamers prefer for aim consistency. Sets Control Panel MouseSpeed to " +
        "'0'; the default ('1') enables acceleration. Stored as a string value; a sign-out may be " +
        "needed for some apps to pick it up.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Mouse",
        ValueName = "MouseSpeed",
        ValueKind = RegistryValueKind.String,
        EnabledValue = "0",
        DisabledValue = "1",
        DefaultValue = "1",
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Mouse!MouseSpeed (string, '0' = off)",
        },
        new()
        {
        Id = "gaming.disable-fullscreen-optimizations",
        Title = "Disable fullscreen optimizations",
        Description = "Forces fullscreen optimizations off system-wide (GameConfigStore GameDVR_FSEBehaviorMode=2) " +
        "so exclusive-fullscreen games bypass the DWM compositor, which can lower input latency. " +
        "The default (0) lets each game decide. May change alt-tab behavior.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"System\GameConfigStore",
        ValueName = "GameDVR_FSEBehaviorMode",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 2,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKCU\\System\\GameConfigStore!GameDVR_FSEBehaviorMode (2 = forced off)",
        },
        new()
        {
        Id = "gaming.disable-active-window-tracking",
        Title = "Disable focus-follows-mouse (active window tracking)",
        Description = "Stops Windows from focusing whatever window the mouse is hovering (ActiveWindowTracking " +
        "under Control Panel\\Desktop), which some games and mice trigger accidentally. The default " +
        "(0) keeps focus on the window you click. Set to 0 to ensure click-to-focus only.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Desktop",
        ValueName = "ActiveWindowTracking",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Desktop!ActiveWindowTracking (0 = click-to-focus)",
        },
        new()
        {
        Id = "gaming.disable-game-bar",
        Title = "Disable Xbox Game Bar capture",
        Description = "Turns off the Xbox Game Bar's background capture and overlay (AppCaptureEnabled=0). The default (1) keeps it on. Disabling frees a little CPU/GPU and removes the Win+G overlay; a sign-out/sign-in may be needed for the change to fully apply.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\GameDVR",
        ValueName = "AppCaptureEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\GameDVR!AppCaptureEnabled (0 = off)",
        },
        new()
        {
        Id = "gaming.disable-mouse-precision",
        Title = "Disable enhanced pointer precision (mouse accel)",
        Description = "Turns off Windows pointer acceleration (MouseSpeed=0 in the mouse sensitivity key) so the cursor moves 1:1 with physical motion, which many gamers prefer for consistency. The default (1) applies acceleration. Note: this changes a shared system mouse setting.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Mouse",
        ValueName = "MouseSpeed",
        ValueKind = RegistryValueKind.String,
        EnabledValue = "0",
        DisabledValue = "1",
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Mouse!MouseSpeed (string, '0' = off)",
        },
    };
}
