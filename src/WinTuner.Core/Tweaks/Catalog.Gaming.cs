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
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers!HwSchMode (2 = on, 1 = off)",
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
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\GameDVR!AllowGameDVR (0 = off, policy)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\GameDVR!AppCaptureEnabled (0 = off)",
        },
        new()
        {
        Id = "gaming.disable-mouse-trails",
        Title = "Disable mouse trail effect",
        Description = "Removes the visual 'mouse trail' comet effect by setting Control Panel Mouse MouseTrails to '0'. The default ('0') already has trails off; this reinforces it. Purely cosmetic and stored as a string value.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Mouse",
        ValueName = "MouseTrails",
        ValueKind = RegistryValueKind.String,
        EnabledValue = "0",
        DisabledValue = "1",
        DefaultValue = "0",
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Mouse!MouseTrails (string, '0' = off)",
        },
        new()
        {
        Id = "gaming.disable-snap-to-default-button",
        Title = "Disable snap-to-default-button",
        Description = "Stops the mouse pointer from automatically jumping to the default button in dialog boxes (Control Panel Mouse SnapToDefaultButton='0'). The default ('0') already leaves it off; this enforces it. A minor accessibility/mouse preference stored as a string value.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Mouse",
        ValueName = "SnapToDefaultButton",
        ValueKind = RegistryValueKind.String,
        EnabledValue = "0",
        DisabledValue = "1",
        DefaultValue = "0",
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Mouse!SnapToDefaultButton (string, '0' = off)",
        },
        new()
        {
        Id = "gaming.disable-game-mode",
        Title = "Disable Game Mode",
        Description = "Turns Game Mode off (GameBar AutoGameModeEnabled=0), stopping Windows from trying to optimize the system for the active game. The default (1) leaves it on. Revert restores the standard on state.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\GameBar",
        ValueName = "AutoGameModeEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\GameBar!AutoGameModeEnabled (0 = off)",
        },
        new()
        {
        Id = "gaming.disable-hardware-gpu-scheduling",
        Title = "Disable hardware-accelerated GPU scheduling",
        Description = "Returns GPU scheduling to the software scheduler (GraphicsDrivers HwSchMode=1), the pre-Windows-10-2004 default. Disabling it can improve stability on GPUs/drivers that misbehave with hardware scheduling. REQUIRES ELEVATION and a reboot. The default (1) is off; 2 enables hardware scheduling.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers",
        ValueName = "HwSchMode",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 2,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers!HwSchMode (1 = software, 2 = hardware)",
        },
        new()
        {
        Id = "gaming.disable-directflip",
        Title = "Disable DirectFlip (overlay flip)",
        Description = "Disables the DirectFlip presentation path (GraphicsDrivers DisableDirectFlip=1), forcing the compositor to copy frames instead of flipping overlays. Can fix flicker/black-screen issues on some GPUs at a small performance cost. REQUIRES ELEVATION and a reboot.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers",
        ValueName = "DisableDirectFlip",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers!DisableDirectFlip (1 = off)",
        },
        new()
        {
        Id = "gaming.disable-game-bar-tips",
        Title = "Disable Game Bar tips",
        Description = "Stops the Xbox Game Bar from showing gameplay tips and pop-ups (GameBar ShowStartupPanel=0). The default (1) shows them on launch. Reduces overlay noise during play.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\GameBar",
        ValueName = "ShowStartupPanel",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\GameBar!ShowStartupPanel (0 = off)",
        },
        new()
        {
        Id = "gaming.disable-hags-conflict",
        Title = "Disable HAGS for capture compatibility",
        Description = "Disables Hardware-accelerated GPU Scheduling (GraphicsDrivers HwSchMode=1) specifically to improve capture/encoding compatibility with older streaming and recording tools. The default (1) is software scheduling; 2 enables HAGS. REQUIRES ELEVATION and a reboot.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers",
        ValueName = "HwSchMode",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 2,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers!HwSchMode (1 = software)",
        },
        new()
        {
        Id = "gaming.enable-triple-buffering",
        Title = "Enable DWM triple buffering (flip)",
        Description = "Turns on the DirectFlip triple-buffering presentation path (GraphicsDrivers FlipBuffers=2) for smoother frame presentation in fullscreen games. The default (1) is double buffering. REQUIRES ELEVATION and a reboot; benefits vary by GPU.",
        Category = TweakCategory.Gaming,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\GraphicsDrivers",
        ValueName = "FlipBuffers",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 2,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\GraphicsDrivers!FlipBuffers (2 = triple)",
        },
    };
}
