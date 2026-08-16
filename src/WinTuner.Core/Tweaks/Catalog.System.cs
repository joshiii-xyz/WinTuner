using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the System category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetSystem() => new List<RegistryTweak>
    {
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
        Reference = "HKLM\\Software\\Policies\\Microsoft\\Windows\\WindowsCopilot!TurnOffWindowsCopilot (policy)",
        },
        new()
        {
        Id = "system.disable-aero-shake",
        Title = "Disable Aero Shake",
        Description = "Stops the 'Aero Shake' behavior where grabbing a window title bar and shaking it minimizes " +
        "all other windows. DisallowShaking=1 disables it; the default (0) leaves it enabled. " +
        "Purely a mouse-gesture preference.",
        Category = TweakCategory.System,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "DisallowShaking",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!DisallowShaking (1 = disabled)",
        },
        new()
        {
        Id = "system.disable-web-search-start",
        Title = "Disable web results in Start search",
        Description = "Removes Bing/web search results from the Start menu and taskbar search box by setting " +
        "BingSearchEnabled=0. The default (1) blends web results with local results. Disabling keeps " +
        "search local-only and avoids sending queries to Microsoft.",
        Category = TweakCategory.System,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Search",
        ValueName = "BingSearchEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Search!BingSearchEnabled (0 = local only)",
        },
        new()
        {
        Id = "system.disable-edge-swipe",
        Title = "Disable touch edge-swipe gestures",
        Description = "Disables the Windows 8/10/11 touch edge-swipe gestures (open Action Center from right edge, " +
        "etc.) via the policy AllowEdgeSwipe=0. The default (absent) leaves them enabled. Useful on " +
        "touch laptops where the gestures trigger accidentally. Requires elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\EdgeUI",
        ValueName = "AllowEdgeSwipe",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\EdgeUI!AllowEdgeSwipe (policy; 0 = disabled)",
        },
        new()
        {
        Id = "system.disable-lock-screen",
        Title = "Disable the lock screen",
        Description = "Skips the Windows lock screen before sign-in via the policy NoLockScreen=1. The default " +
        "(absent) shows the lock screen. Requires elevation; some Windows editions ignore this " +
        "policy on the sign-in screen.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\Personalization",
        ValueName = "NoLockScreen",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Personalization!NoLockScreen (policy; 1 = disabled)",
        },
        new()
        {
        Id = "system.disable-consumer-features",
        Title = "Disable Windows consumer experiences",
        Description = "Blocks consumer marketing/experiences (e.g. suggested apps, third-party promotions) via " +
        "the policy CloudContent DisableWindowsConsumerFeatures=1. The default (absent) allows " +
        "them. Requires elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\CloudContent",
        ValueName = "DisableWindowsConsumerFeatures",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\CloudContent!DisableWindowsConsumerFeatures (policy; 1 = off)",
        },
        new()
        {
        Id = "system.disable-softlanding",
        Title = "Disable Windows Spotlight / SoftLanding tips",
        Description = "Turns off the rotating Windows Spotlight content and SoftLanding promotional tips via the " +
        "policy CloudContent DisableSoftLanding=1. The default (absent) allows them. Requires " +
        "elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\CloudContent",
        ValueName = "DisableSoftLanding",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\CloudContent!DisableSoftLanding (policy; 1 = off)",
        },
        new()
        {
        Id = "system.disable-cortana",
        Title = "Disable Cortana",
        Description = "Removes Cortana from search and the system via the policy Windows Search AllowCortana=0. " +
        "The default (absent) allows it. Requires elevation; Reset deletes the policy. Note: " +
        "modern Windows already separates Cortana from search for many users.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\Windows Search",
        ValueName = "AllowCortana",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search!AllowCortana (policy; 0 = disabled)",
        },
        new()
        {
        Id = "system.disable-auto-reboot-updates",
        Title = "Don't auto-reboot while logged on (updates)",
        Description = "Stops Windows from automatically rebooting to finish updates while you are logged on, via " +
        "the policy WindowsUpdate AU NoAutoRebootWithLoggedOnUsers=1. The default (absent) allows " +
        "scheduled reboots. Requires elevation; you are still prompted to reboot manually.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU",
        ValueName = "NoAutoRebootWithLoggedOnUsers",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU!NoAutoRebootWithLoggedOnUsers (policy; 1 = off)",
        },
        new()
        {
        Id = "system.disable-toast-notifications",
        Title = "Disable toast notifications",
        Description = "Turns off the balloon/toast notifications from the action center by setting PushNotifications " +
        "ToastEnabled=0. The default (1) shows app and system toasts. Quiet but you may miss alerts.",
        Category = TweakCategory.System,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\PushNotifications",
        ValueName = "ToastEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\PushNotifications!ToastEnabled (0 = off)",
        },
        new()
        {
        Id = "system.disable-ink-workspace",
        Title = "Disable Windows Ink Workspace",
        Description = "Removes the Windows Ink Workspace (pen/drawing shortcuts) via the policy " +
        "AllowWindowsInkWorkspace=0. The default (1) shows it on pen-capable devices. Requires " +
        "elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\WindowsInkWorkspace",
        ValueName = "AllowWindowsInkWorkspace",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\WindowsInkWorkspace!AllowWindowsInkWorkspace (policy; 0 = off)",
        },
        new()
        {
        Id = "system.disable-auto-maintenance",
        Title = "Disable automatic maintenance",
        Description = "Stops Windows from running its scheduled automatic maintenance (defrag, updates, " +
        "diagnostics) in the background by setting Maintenance MaintenanceDisabled=1. The default " +
        "(0) lets it run during idle. Requires elevation; you can still trigger maintenance " +
        "manually.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Schedule\Maintenance",
        ValueName = "MaintenanceDisabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Schedule\\Maintenance!MaintenanceDisabled (1 = off)",
        },
        new()
        {
        Id = "system.disable-fast-startup",
        Title = "Disable fast startup",
        Description = "Turns off fast startup (HiberbootEnabled=0), which otherwise hibernates the kernel on shutdown so boot is faster. Disabling means a true cold boot each time, which helps when drivers or devices misbehave after shutdown. REQUIRES ELEVATION and a reboot. The default (1) enables fast startup.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Power",
        ValueName = "HiberbootEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Power!HiberbootEnabled (0 = off)",
        },
        new()
        {
        Id = "system.disable-driver-updates-via-wu",
        Title = "Exclude driver updates from Windows Update",
        Description = "Stops Windows Update from automatically installing driver updates by setting the policy WindowsUpdate ExcludeWUDriversInQualityUpdate=1. The default (absent/0) lets Windows update drivers. Requires elevation; useful when a specific vendor driver is preferred over Microsoft's.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate",
        ValueName = "ExcludeWUDriversInQualityUpdate",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate!ExcludeWUDriversInQualityUpdate (policy; 1 = exclude)",
        },
        new()
        {
        Id = "system.disable-startup-sound",
        Title = "Disable the startup sound",
        Description = "Suppresses the Windows startup sound by setting the policy System DisableStartupSound=1. The default (absent) plays the sound during boot. Requires elevation; purely cosmetic and does not affect other sounds.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
        ValueName = "DisableStartupSound",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System!DisableStartupSound (policy; 1 = off)",
        },
        new()
        {
        Id = "system.disable-fast-user-switching",
        Title = "Disable fast user switching",
        Description = "Hides the Switch user option from the sign-in screen and Start by setting the policy System HideFastUserSwitching=1, so only one interactive session is active at a time. The default (absent) shows the switcher. Requires elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
        ValueName = "HideFastUserSwitching",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System!HideFastUserSwitching (policy; 1 = off)",
        },
        new()
        {
        Id = "system.disable-store-autoupdate",
        Title = "Disable Microsoft Store auto-download",
        Description = "Stops the Microsoft Store from automatically downloading and installing app updates in the background by setting the policy WindowsStore AutoDownload=2. The default (absent/1) auto-updates. Requires elevation; you update apps manually from the Store.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\WindowsStore",
        ValueName = "AutoDownload",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 2,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\WindowsStore!AutoDownload (policy; 2 = never)",
        },
        new()
        {
        Id = "system.disable-cortana-above-lock",
        Title = "Disable Cortana above the lock screen",
        Description = "Prevents the Cortana/voice assistant from being available above the lock screen by setting the policy Windows Search AllowCortanaAboveLock=0. The default (absent) allows it. Requires elevation; a minor privacy hardening on shared machines.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\Windows Search",
        ValueName = "AllowCortanaAboveLock",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Search!AllowCortanaAboveLock (policy; 0 = off)",
        },
        new()
        {
        Id = "system.enable-long-file-paths",
        Title = "Enable long file paths (260+ chars)",
        Description = "Removes the legacy 260-character path limit for applications that opt in, by setting FileSystem LongPathsEnabled=1. The default (0) enforces the old MAX_PATH limit. Requires elevation; helps deep folder hierarchies and build tools.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\FileSystem",
        ValueName = "LongPathsEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\FileSystem!LongPathsEnabled (1 = enabled)",
        },
        new()
        {
        Id = "system.disable-windows-defender-smartscreen-explorer",
        Title = "Disable SmartScreen for File Explorer",
        Description = "Stops SmartScreen from checking files you open in Explorer against Microsoft's reputation service (via the policy Explorer EnableSmartScreen=0). The default (absent) checks files at open. Disabling removes a layer of protection against malicious downloads. Requires elevation.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\System",
        ValueName = "EnableSmartScreen",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\System!EnableSmartScreen (policy; 0 = off)",
        },
        new()
        {
        Id = "system.disable-storage-sense",
        Title = "Disable Storage Sense",
        Description = "Turns off Storage Sense, the background feature that auto-deletes temp files and empties the Recycle Bin on a schedule, by setting StoragePolicy 01=0. The default (1) lets it run. Disabling keeps your manual control over cleanup.",
        Category = TweakCategory.System,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\StoragePolicies",
        ValueName = "01",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\StoragePolicies!01 (0 = off)",
        },
        new()
        {
        Id = "system.disable-sleep-timeout",
        Title = "Never sleep when plugged in (AC)",
        Description = "Sets the AC (plugged-in) sleep timeout to 0 (never) via Power Scheme Settings 29f6c1db-86da-48c5-9fdb-f2b67b1f44da, so a desktop or docked laptop won't sleep on its own while powered. The default varies by scheme. Requires elevation; battery behavior is unaffected when on DC.",
        Category = TweakCategory.System,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Power\PowerSettings\238C9FA8-0AAD-41ED-83F4-97BE242C8F20\29f6c1db-86da-48c5-9fdb-f2b67b1f44da",
        ValueName = "ACSettingIndex",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 3600,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\SYSTEM\\CurrentControlSet\\Control\\Power\\PowerSettings\\238C9FA8-0AAD-41ED-83F4-97BE242C8F20\\29f6c1db-86da-48c5-9fdb-f2b67b1f44da!ACSettingIndex (0 = never)",
        },
    };
}
