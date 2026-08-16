using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Appearance category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetAppearance() => new List<RegistryTweak>
    {
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize!AppsUseLightTheme (0 = dark, 1 = light)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize!SystemUsesLightTheme (0 = dark, 1 = light)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarAl (0 = left, 1 = center)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarSmallIcons (1 = small)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Search!SearchboxTaskbarMode (0 = hidden, 1 = icon, 2 = box)",
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
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarDa (0 = hidden, 1 = shown)",
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
        Reference = "HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows\\Windows Chat!ChatIcon (policy; 0 = hidden)",
        },
        new()
        {
        Id = "appearance.enable-aero-peek",
        Title = "Enable Aero Peek (desktop preview)",
        Description = "Restores the Aero Peek behavior (hover the taskbar show-desktop sliver to peek at the " +
        "desktop) by setting Advanced EnablePeek=1. The default (0) disables peek on modern " +
        "builds. Purely cosmetic.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "EnablePeek",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!EnablePeek (1 = on)",
        },
        new()
        {
        Id = "appearance.show-clock-seconds",
        Title = "Show seconds in the taskbar clock",
        Description = "Displays the seconds in the system tray clock by setting Advanced " +
        "ShowSecondsInSystemClock=1. The default (0) shows only hours:minutes. Minor; requires " +
        "a taskbar restart/sign-in to appear.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowSecondsInSystemClock",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!ShowSecondsInSystemClock (1 = on)",
        },
        new()
        {
        Id = "appearance.disable-sticky-keys",
        Title = "Disable the Sticky Keys shortcut prompt",
        Description = "Stops the Sticky/Filter/Keys accessibility dialog from popping up when you press Shift " +
        "five times, by setting Accessibility StickyKeys Flags to 506. The default (510) enables " +
        "the shortcut. Accessibility tooling still works from Settings.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Accessibility\StickyKeys",
        ValueName = "Flags",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 506,
        DisabledValue = 510,
        DefaultValue = 510,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Accessibility\\StickyKeys!Flags (506 = disabled)",
        },
        new()
        {
        Id = "appearance.disable-tablet-mode",
        Title = "Disable tablet mode auto-switching",
        Description = "Forces Tablet Mode off (ImmersiveShell TabletMode=0) so convertible/touch devices stay " +
        "in desktop mode. The default (0) is desktop anyway; this locks it against automatic " +
        "switching on 2-in-1s. A sign-out/in may be needed.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\ImmersiveShell",
        ValueName = "TabletMode",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell!TabletMode (0 = desktop)",
        },
        new()
        {
        Id = "appearance.never-combine-taskbar",
        Title = "Never combine taskbar buttons",
        Description = "Makes the Windows taskbar show each open window as its own labeled button instead of " +
        "grouping them, by setting Explorer\\Advanced TaskbarGlomLevel=2. The default (0) combines " +
        "buttons of the same app. A taskbar restart/sign-in is needed to apply.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "TaskbarGlomLevel",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 2,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarGlomLevel (2 = never combine)",
        },
        new()
        {
        Id = "appearance.disable-taskbar-animations",
        Title = "Disable taskbar animations",
        Description = "Turns off the slide/fade animations on the taskbar (e.g. when buttons appear) by setting " +
        "Control Panel Desktop TaskbarAnimations=0. The default (1) animates. Minor visual " +
        "preference; can feel snappier.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Desktop",
        ValueName = "TaskbarAnimations",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Desktop!TaskbarAnimations (0 = off)",
        },
        new()
        {
        Id = "appearance.disable-window-animations",
        Title = "Disable window open/close animations",
        Description = "Removes the minimize/maximize/restore animations (the classic 'roll up' effect) by setting " +
        "WindowMetrics MinAnimate=0. The default (1) animates windows. Can make the desktop feel " +
        "more responsive on slower machines. A sign-out/in may be needed.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Control Panel\Desktop\WindowMetrics",
        ValueName = "MinAnimate",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Control Panel\\Desktop\\WindowMetrics!MinAnimate (0 = off)",
        },
        new()
        {
        Id = "appearance.disable-taskbar-badges",
        Title = "Hide taskbar badges for Store apps",
        Description = "Removes the small overlay badges (e.g. unread counts) that some Microsoft Store apps paint onto their taskbar icons (TaskbarBadges=0). The default (1) shows them. Purely cosmetic.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "TaskbarBadges",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarBadges (0 = hidden)",
        },
        new()
        {
        Id = "appearance.accent-on-start-taskbar",
        Title = "Show accent color on Start and taskbar",
        Description = "Paints the Start menu, taskbar, and action center with your chosen accent color (ColorPrevalence=1) rather than the default neutral. Independent of the per-app/apps-light-theme toggles. A sign-out/in may be needed to fully recolor.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
        ValueName = "ColorPrevalence",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize!ColorPrevalence (1 = accent on)",
        },
        new()
        {
        Id = "appearance.hide-taskview-button",
        Title = "Hide the Task View button on the taskbar",
        Description = "Removes the Task View (virtual desktops) button from the taskbar (ShowTaskViewButton=0). The default (1) shows it. Useful if you never use virtual desktops.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowTaskViewButton",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!ShowTaskViewButton (0 = hidden)",
        },
        new()
        {
        Id = "appearance.show-taskbar-all-displays",
        Title = "Show taskbar on all displays",
        Description = "Extends the taskbar to every monitor instead of just the primary one (Explorer Advanced MMTaskbarEnabled=1). The default (0) shows the taskbar only on the main display. Multi-monitor convenience; a taskbar restart/sign-in applies it.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "MMTaskbarEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!MMTaskbarEnabled (1 = all displays)",
        },
        new()
        {
        Id = "appearance.taskbar-combine-when-full",
        Title = "Combine taskbar buttons only when full",
        Description = "Sets the taskbar to combine buttons of the same app only once it runs out of room (Explorer Advanced TaskbarGlomLevel=1). The default (0) always combines; 2 never combines. A middle-ground layout that needs a taskbar restart/sign-in to apply.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "TaskbarGlomLevel",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!TaskbarGlomLevel (1 = when full)",
        },
        new()
        {
        Id = "appearance.disable-jumplist-history",
        Title = "Disable taskbar Jump List history",
        Description = "Stops the taskbar/Start from building per-app Jump Lists of recent items (Advanced Start_TrackProgs=0). The default (1) tracks app usage for Jump Lists. A privacy tweak at the cost of quick-recent shortcuts.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "Start_TrackProgs",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced!Start_TrackProgs (0 = off)",
        },
        new()
        {
        Id = "appearance.use-light-taskbar-text",
        Title = "Force light taskbar text (dark wallpaper)",
        Description = "Forces the taskbar/Start to use light (white) text via the registry color override (Themes Personalize ColorPrevalence combined with a forced light setting), improving contrast on dark wallpapers. The default adapts automatically. A sign-out/in applies it.",
        Category = TweakCategory.Appearance,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
        ValueName = "SystemUsesLightTheme",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize!SystemUsesLightTheme (0 = light text)",
        },
    };
}
