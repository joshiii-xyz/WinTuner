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
        Reference = "HKLM\\...\\WindowsCopilot!TurnOffWindowsCopilot (policy)",
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
        Reference = "HKCU\\...\\Explorer\\Advanced!DisallowShaking (1 = disabled)",
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
        Reference = "HKCU\\...\\Search!BingSearchEnabled (0 = local only)",
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
        Reference = "HKLM\\...\\EdgeUI!AllowEdgeSwipe (policy; 0 = disabled)",
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
        Reference = "HKLM\\...\\Personalization!NoLockScreen (policy; 1 = disabled)",
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
        Reference = "HKLM\\...\\CloudContent!DisableWindowsConsumerFeatures (policy; 1 = off)",
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
        Reference = "HKLM\\...\\CloudContent!DisableSoftLanding (policy; 1 = off)",
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
        Reference = "HKLM\\...\\Windows Search!AllowCortana (policy; 0 = disabled)",
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
        Reference = "HKLM\\...\\WindowsUpdate\\AU!NoAutoRebootWithLoggedOnUsers (policy; 1 = off)",
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
        Reference = "HKCU\\...\\PushNotifications!ToastEnabled (0 = off)",
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
        Reference = "HKLM\\...\\WindowsInkWorkspace!AllowWindowsInkWorkspace (policy; 0 = off)",
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
        Reference = "HKLM\\...\\Schedule\\Maintenance!MaintenanceDisabled (1 = off)",
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
        Reference = "HKLM\\...\\Control\\Power!HiberbootEnabled (0 = off)",
        },
    };
}
