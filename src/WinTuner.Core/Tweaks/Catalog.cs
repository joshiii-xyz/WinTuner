using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// The full catalog of known tweaks. Every entry is declarative data.
/// Descriptions state exactly what registry value changes; they do not speculate
/// about unverified side effects. New tweaks are added here only - no code changes.
/// </summary>
public static class Catalog
{
    public static IReadOnlyList<RegistryTweak> All { get; } = new List<RegistryTweak>
    {
        new()
        {
            Id = "explorer.show-file-extensions",
            Title = "Show file name extensions",
            Description = "Makes File Explorer display the file-type extension (e.g. .txt, .exe) for every file. " +
                          "Windows hides extensions by default, which makes it harder to tell real file types apart " +
                          "and easier to be fooled by a malicious file named 'invoice.pdf.exe'.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "HideFileExt",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!HideFileExt (0 = show, 1 = hide)",
        },
        new()
        {
            Id = "explorer.show-hidden-files",
            Title = "Show hidden files, folders, and drives",
            Description = "Reveals files and folders marked with the hidden attribute in File Explorer. " +
                          "The OS default hides them (value 2). Setting the value to 1 shows hidden items so you can " +
                          "see and manage configuration/dotfiles that Windows normally conceals.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "Hidden",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 2,
            DefaultValue = 2,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!Hidden (1 = show, 2 = hide)",
        },
        new()
        {
            Id = "explorer.hide-drive-letters",
            Title = "Hide empty drive letters",
            Description = "Hides the drive letter label (e.g. C:) next to drives in File Explorer when the drive has " +
                          "no media. Value 1 hides the letter for empty drives; 0 always shows it. Default is shown (0).",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "HideDrivesWithNoMedia",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!HideDrivesWithNoMedia",
        },
        new()
        {
            Id = "privacy.disable-advertising-id",
            Title = "Disable advertising ID",
            Description = "Turns off the per-device advertising ID that apps (particularly Store apps) can use to serve " +
                          "personalized ads across apps. Value 0 disables it (recommended for privacy); the OS default " +
                          "enables it (1).",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\AdvertisingInfo",
            ValueName = "Enabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\AdvertisingInfo!Enabled",
        },
        new()
        {
            Id = "privacy.disable-tailored-experiences",
            Title = "Disable tailored experiences with diagnostic data",
            Description = "Stops Windows from using your diagnostic data to personalize tips, ads, and recommendations " +
                          "in the OS. Setting the value to 0 disables the tailored-experiences feature found under " +
                          "Privacy settings. Requires a sign-out/sign-in to take full effect.",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Privacy",
            ValueName = "TailoredExperiencesWithDiagnosticDataEnabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Privacy!TailoredExperiencesWithDiagnosticDataEnabled",
        },
        new()
        {
            Id = "system.disable-windows-copilot",
            Title = "Disable Windows Copilot",
            Description = "Removes the Windows Copilot entry point via the administrative policy key. Setting " +
                          "TurnOffWindowsCopilot to 1 disables Copilot on the taskbar and the Win+C shortcut. " +
                          "Writes to HKLM, so the app must be running as administrator to apply this one.",
            Category = TweakCategory.System,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"Software\Policies\Microsoft\Windows\WindowsCopilot",
            ValueName = "TurnOffWindowsCopilot",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\WindowsCopilot!TurnOffWindowsCopilot (policy)",
        },
        new()
        {
            Id = "performance.disable-transparency",
            Title = "Disable transparency effects",
            Description = "Turns off the acrylic/transparency effects used by the taskbar, Start menu, and " +
                          "some surfaces. Disabling them can reduce GPU compositing work on low-end or " +
                          "integrated-GPU systems, at the cost of the translucent look. Purely visual; no " +
                          "functionality is lost.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            ValueName = "EnableTransparency",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Personalize!EnableTransparency (0 = off, 1 = on)",
        },
        new()
        {
            Id = "performance.disable-startup-delay",
            Title = "Disable startup program delay",
            Description = "Removes the built-in delay Windows adds before launching startup apps, so programs " +
                          "in your Startup folder open sooner after sign-in. Writes StartupDelayInMSec=0; revert " +
                          "restores a delay, and Reset deletes the value to return to the OS default.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Serialize",
            ValueName = "StartupDelayInMSec",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 20000,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Serialize!StartupDelayInMSec (0 = no delay)",
        },
        new()
        {
            Id = "performance.disable-gamedvr-background",
            Title = "Disable Game DVR background recording",
            Description = "Turns off the Xbox Game Bar's background recording ('record in background while I'm " +
                          "playing'). Disabling frees CPU, GPU, and disk during gameplay. A sign-out/sign-in or " +
                          "Game Bar restart is needed for the change to fully take effect.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"System\GameConfigStore",
            ValueName = "GameDVR_Enabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\System\\GameConfigStore!GameDVR_Enabled (0 = off, 1 = on)",
        },
        new()
        {
            Id = "performance.disable-sysmain",
            Title = "Disable SysMain (Superfetch)",
            Description = "Sets the SysMain service (formerly Superfetch) Start value to 4 (disabled). SysMain " +
                          "preloads frequently used memory into RAM; disabling it can help on systems with " +
                          "limited RAM or an SSD where prefetching adds little benefit. REQUIRES ELEVATION and a " +
                          "reboot to take effect. Reset deletes the value to restore the default service start.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\SysMain",
            ValueName = "Start",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 4,
            DisabledValue = 2,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Services\\SysMain!Start (4 = disabled)",
        },
        new()
        {
            Id = "appearance.dark-mode",
            Title = "Enable dark mode for apps",
            Description = "Switches Windows apps to the dark color scheme by setting AppsUseLightTheme to 0. The " +
                          "taskbar/Start shell has a separate toggle (SystemUsesLightTheme); this affects apps and " +
                          "many system surfaces. A sign-out/in may be needed for some surfaces to recolor.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            ValueName = "AppsUseLightTheme",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Personalize!AppsUseLightTheme (0 = dark, 1 = light)",
        },
        new()
        {
            Id = "appearance.accent-on-titlebars",
            Title = "Show accent color on title bars",
            Description = "Paints the active window's title bar with your chosen accent color instead of the " +
                          "default white (light) or black (dark). Purely cosmetic and applied via the DWM " +
                          "ColorPrevalence value.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\DWM",
            ValueName = "ColorPrevalence",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\Software\\Microsoft\\Windows\\DWM!ColorPrevalence (1 = accent)",
        },
        new()
        {
            Id = "appearance.taskbar-left",
            Title = "Align taskbar to the left",
            Description = "Moves Windows 11 taskbar icons to the left edge, matching the classic Windows 10 " +
                          "layout, by setting TaskbarAl to 0. The Windows 11 default is centered (1).",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "TaskbarAl",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Advanced!TaskbarAl (0 = left, 1 = center)",
        },
        new()
        {
            Id = "appearance.small-taskbar",
            Title = "Use small taskbar buttons",
            Description = "Switches the taskbar to smaller icons by setting TaskbarSmallIcons to 1, giving more " +
                          "room for open windows. The default is normal-sized icons (0).",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "TaskbarSmallIcons",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Advanced!TaskbarSmallIcons (1 = small)",
        },
        new()
        {
            Id = "appearance.hide-taskbar-search",
            Title = "Hide the taskbar search box",
            Description = "Removes the search box/icon from the taskbar by setting SearchboxTaskbarMode to 0. " +
                          "Revert restores the search icon (1); the full search box is value 2. Search remains " +
                          "available from the Start menu.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Search",
            ValueName = "SearchboxTaskbarMode",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Search!SearchboxTaskbarMode (0 = hidden, 1 = icon, 2 = box)",
        },
        new()
        {
            Id = "security.disable-smartscreen-apps",
            Title = "Disable SmartScreen for apps & files",
            Description = "WARNING: SmartScreen blocks known malicious apps and files at launch and warns about " +
                          "unrecognized downloads. Disabling it (policy EnableSmartScreen=0) removes a layer of " +
                          "protection against phishing and malware. Only disable if you fully understand the " +
                          "risk. Requires elevation; Reset deletes the policy to restore protection.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SOFTWARE\Policies\Microsoft\Windows\System",
            ValueName = "EnableSmartScreen",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\System!EnableSmartScreen (policy; absent = on)",
        },
        new()
        {
            Id = "security.disable-autorun",
            Title = "Disable AutoRun on removable drives",
            Description = "Prevents Windows from automatically running programs from inserted USB/removable media " +
                          "by setting NoDriveTypeAutoRun to 0xFF. This blocks a common malware propagation path. " +
                          "The default (0x91) still auto-opens some drive types.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer",
            ValueName = "NoDriveTypeAutoRun",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 255,
            DisabledValue = 145,
            DefaultValue = 145,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Policies\\Explorer!NoDriveTypeAutoRun (0xFF = off)",
        },
        new()
        {
            Id = "security.disable-insecure-guest",
            Title = "Disable insecure guest logons (SMB)",
            Description = "Blocks SMB client support for insecure guest logons (AllowInsecureGuestAuth=0), closing " +
                          "a path that can be abused on untrusted networks. The secure default is already 0; this " +
                          "ensures it stays off. Requires elevation.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
            ValueName = "AllowInsecureGuestAuth",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Lsa!AllowInsecureGuestAuth (0 = blocked)",
        },
        new()
        {
            Id = "network.disable-ipv6",
            Title = "Disable IPv6 (all interfaces)",
            Description = "Fully disables IPv6 by setting Tcpip6 DisabledComponents to 0xFF. Useful on networks " +
                          "where IPv6 causes issues, but it can break features that require IPv6 (e.g. some " +
                          "Microsoft services). REQUIRES ELEVATION and a reboot. Reset re-enables IPv6 (0).",
            Category = TweakCategory.Network,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters",
            ValueName = "DisabledComponents",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 255,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Tcpip6\\Parameters!DisabledComponents (0xFF = off)",
        },
        new()
        {
            Id = "network.disable-lmhosts",
            Title = "Disable LMHOSTS lookup",
            Description = "Turns off the NetBIOS LMHOSTS file lookup (EnableLMHOSTS=0), a legacy name-resolution " +
                          "mechanism rarely needed on modern networks. Minor; requires elevation to apply.",
            Category = TweakCategory.Network,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\NetBT\Parameters",
            ValueName = "EnableLMHOSTS",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\NetBT\\Parameters!EnableLMHOSTS (0 = off)",
        },
        new()
        {
            Id = "network.disable-netbios",
            Title = "Disable NetBIOS over TCP/IP",
            Description = "Sets the global NetbiosOptions to 2 (disabled), turning off NetBIOS-over-TCP/IP, which " +
                          "closes old SMB name-service attack surface. Per-adapter DHCP settings can override " +
                          "this per interface. Requires elevation.",
            Category = TweakCategory.Network,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\NetBT\Parameters",
            ValueName = "NetbiosOptions",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 2,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\NetBT\\Parameters!NetbiosOptions (2 = disabled)",
        },
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
    };
}
