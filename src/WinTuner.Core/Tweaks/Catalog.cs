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
        // ===================== EXPLORER =====================
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
            Id = "explorer.show-protected-os-files",
            Title = "Show protected operating-system files",
            Description = "Reveals files flagged as protected operating-system files (e.g. pagefile, boot records) in " +
                          "File Explorer when 'Show hidden files' is also on. ShowSuperHidden=1 reveals them; the " +
                          "default (0) keeps them hidden. These files should normally be left alone.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "ShowSuperHidden",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!ShowSuperHidden (1 = show)",
        },
        new()
        {
            Id = "explorer.disable-thumbnails",
            Title = "Disable thumbnails (icons only)",
            Description = "Stops Explorer from generating thumbnail previews and shows generic file-type icons " +
                          "instead (IconsOnly=1). This reduces disk/indexer activity on large media folders but " +
                          "removes the preview images. The default (0) shows thumbnails.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "IconsOnly",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!IconsOnly (1 = icons only)",
        },
        new()
        {
            Id = "explorer.launch-to-this-pc",
            Title = "Launch File Explorer to This PC",
            Description = "Makes File Explorer open on 'This PC' instead of the default Quick Access view. " +
                          "LaunchTo=1 opens This PC; the Windows default (0) opens Quick Access. Purely a " +
                          "convenience/navigation preference.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "LaunchTo",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!LaunchTo (1 = This PC, 0 = Quick Access)",
        },
        new()
        {
            Id = "explorer.hide-recent-quick-access",
            Title = "Hide recent files in Quick Access",
            Description = "Removes the 'Recent files' section from the Quick Access pane in File Explorer. " +
                          "ShowRecent=0 hides them (improves privacy on shared machines); the default (1) shows " +
                          "your recently opened files there.",
            Category = TweakCategory.Explorer,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer",
            ValueName = "ShowRecent",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer!ShowRecent (0 = hidden, 1 = shown)",
        },

        // ===================== PRIVACY =====================
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
            Id = "privacy.disable-start-suggestions",
            Title = "Disable suggested content on Start",
            Description = "Removes the 'suggested' app/ content rows that Windows injects into the Start menu via " +
                          "ContentDeliveryManager (SubscribedContent-338388=0). The default (1) lets Windows promote " +
                          "apps and content there. Improves a clean Start layout.",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
            ValueName = "SubscribedContent-338388",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\ContentDeliveryManager!SubscribedContent-338388 (0 = off)",
        },
        new()
        {
            Id = "privacy.disable-windows-tips",
            Title = "Disable Windows tips and Spotlight suggestions",
            Description = "Stops Windows from showing tips, the Spotlight desktop wallpaper, and 'fun facts' " +
                          "notifications (SoftLandingEnabled=0). The default (1) enables these consumer suggestions. " +
                          "Disabling reduces unsolicited pop-ups and background content rotation.",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
            ValueName = "SoftLandingEnabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\ContentDeliveryManager!SoftLandingEnabled (0 = off)",
        },
        new()
        {
            Id = "privacy.disable-feedback-notifications",
            Title = "Disable Windows feedback notifications",
            Description = "Suppresses the periodic 'feedback' pop-ups asking you to rate Windows, via the administrative " +
                          "policy DoNotShowFeedbackNotifications=1. The default (absent) lets Windows show them. " +
                          "Writes to HKLM, so the app must be running as administrator to apply this one.",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SOFTWARE\Policies\Microsoft\Windows\DataCollection",
            ValueName = "DoNotShowFeedbackNotifications",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\DataCollection!DoNotShowFeedbackNotifications (policy)",
        },

        // ===================== SYSTEM =====================
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

        // ===================== PERFORMANCE =====================
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
            Id = "performance.disable-hibernation",
            Title = "Disable hibernation",
            Description = "Turns off hibernation by setting HibernateEnabled=0, which also removes the hiberfil.sys " +
                          "file from the system drive and frees that disk space. REQUIRES ELEVATION and a reboot. " +
                          "The default (1) keeps hibernation available for fast startup and resume.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Control\Power",
            ValueName = "HibernateEnabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Control\\Power!HibernateEnabled (0 = off)",
        },
        new()
        {
            Id = "performance.disable-background-apps",
            Title = "Disable background apps",
            Description = "Stops most Microsoft Store / UWP apps from running in the background (GlobalUserDisabled=1), " +
                          "cutting idle CPU, network, and battery usage. The default (0) lets apps run background " +
                          "tasks like mail sync and live tiles.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\BackgroundAccessApplications",
            ValueName = "GlobalUserDisabled",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\BackgroundAccessApplications!GlobalUserDisabled (1 = off)",
        },
        new()
        {
            Id = "performance.visual-effects-best-performance",
            Title = "Adjust visual effects for best performance",
            Description = "Tells Windows to favor performance over eye-candy by setting VisualFXSetting=2 ('Adjust " +
                          "for best performance'), disabling animations and shadows. 1 = best appearance, 0 = let " +
                          "Windows choose. Some changes need a sign-out to fully apply.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects",
            ValueName = "VisualFXSetting",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 2,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\VisualEffects!VisualFXSetting (2 = best perf)",
        },
        new()
        {
            Id = "performance.reduce-menu-show-delay",
            Title = "Reduce menu show delay",
            Description = "Lowers the delay before cascading/submenus open by setting MenuShowDelay to '0' (milliseconds) " +
                          "in the Control Panel Desktop key. The default is '400'. Smaller values make menus feel more " +
                          "responsive. Stored as a string value.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Control Panel\Desktop",
            ValueName = "MenuShowDelay",
            ValueKind = RegistryValueKind.String,
            EnabledValue = "0",
            DisabledValue = "400",
            DefaultValue = "400",
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\Control Panel\\Desktop!MenuShowDelay (string, '0' = instant)",
        },

        // ===================== APPEARANCE =====================
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
            Id = "appearance.system-dark-mode",
            Title = "Enable dark mode for system (Start/taskbar)",
            Description = "Switches the shell surfaces - Start menu, taskbar, action center, and system tray - to dark " +
                          "by setting SystemUsesLightTheme to 0. This is independent of the per-app dark mode above. " +
                          "A sign-out/in may be needed for the taskbar to recolor fully.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            ValueName = "SystemUsesLightTheme",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Personalize!SystemUsesLightTheme (0 = dark, 1 = light)",
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
            Id = "appearance.hide-widgets",
            Title = "Hide the Widgets taskbar button",
            Description = "Removes the Windows 11 Widgets (weather/news) button from the taskbar by setting " +
                          "TaskbarDa to 0. The default (1) shows the button. Widgets can still be opened from " +
                          "the touch/Win shortcut if desired.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.CurrentUser,
            SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
            ValueName = "TaskbarDa",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKCU\\...\\Explorer\\Advanced!TaskbarDa (0 = hidden, 1 = shown)",
        },
        new()
        {
            Id = "appearance.disable-chat-button",
            Title = "Hide the Chat (Teams) taskbar button",
            Description = "Removes the Windows 11 Chat/Teams button from the taskbar via the policy ChatIcon=0. " +
                          "The default (absent) shows the button. Requires elevation to write the policy key.",
            Category = TweakCategory.Appearance,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SOFTWARE\Policies\Microsoft\Windows\Windows Chat",
            ValueName = "ChatIcon",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Windows Chat!ChatIcon (policy; 0 = hidden)",
        },

        // ===================== SECURITY =====================
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
            Id = "security.clear-pagefile-shutdown",
            Title = "Clear page file at shutdown",
            Description = "Forces Windows to wipe the paging file (pagefile.sys) when the system shuts down by setting " +
                          "ClearPageFileAtShutdown=1, so sensitive data left in the page file is not recoverable from " +
                          "disk. The default (0) skips the wipe. Requires elevation; adds a small shutdown delay.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management",
            ValueName = "ClearPageFileAtShutdown",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Memory Management!ClearPageFileAtShutdown (1 = wipe)",
        },
        new()
        {
            Id = "security.disable-remote-registry",
            Title = "Disable Remote Registry service",
            Description = "Sets the Remote Registry service Start value to 4 (disabled), preventing remote users from " +
                          "editing the local registry over the network. Most home users never need it. REQUIRES " +
                          "ELEVATION and a reboot. Reset deletes the value to restore the default service start.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\RemoteRegistry",
            ValueName = "Start",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 4,
            DisabledValue = 2,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Services\\RemoteRegistry!Start (4 = disabled)",
        },
        new()
        {
            Id = "security.disable-upnp-host",
            Title = "Disable UPnP Device Host service",
            Description = "Sets the UPnP Device Host service (upnphost) Start value to 4 (disabled). UPnP discovery " +
                          "can expand the local network attack surface; disabling it is reasonable on trusted, static " +
                          "networks. REQUIRES ELEVATION and a reboot. Reset deletes the value to restore defaults.",
            Category = TweakCategory.Security,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\upnphost",
            ValueName = "Start",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 4,
            DisabledValue = 2,
            DefaultValue = null,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\Services\\upnphost!Start (4 = disabled)",
        },

        // ===================== NETWORK =====================
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
            Id = "network.disable-delivery-optimization",
            Title = "Disable Windows Update Delivery Optimization",
            Description = "Stops Windows from peer-to-peer sharing of updates (and Microsoft Store content) with " +
                          "other machines by setting DODownloadMode=0 (HTTP only from Microsoft). The default (1/3) " +
                          "allows peer sharing that uses extra bandwidth. Requires elevation.",
            Category = TweakCategory.Network,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config",
            ValueName = "DODownloadMode",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\DeliveryOptimization\\Config!DODownloadMode (0 = off)",
        },
        new()
        {
            Id = "network.disable-smbv1",
            Title = "Disable SMBv1 protocol",
            Description = "Turns off the legacy SMBv1 file-sharing protocol (LanmanServer SMB1=0). SMBv1 is " +
                          "decades old, unencrypted, and was the vector behind WannaCry; Microsoft strongly " +
                          "recommends disabling it. Requires elevation; a reboot fully removes the component.",
            Category = TweakCategory.Network,
            Hive = RegistryHive.LocalMachine,
            SubKey = @"SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
            ValueName = "SMB1",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 0,
            DisabledValue = 1,
            DefaultValue = 1,
            AbsentState = TweakState.Disabled,
            Reference = "HKLM\\...\\LanmanServer\\Parameters!SMB1 (0 = disabled)",
        },

        // ===================== GAMING =====================
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
    };
}
