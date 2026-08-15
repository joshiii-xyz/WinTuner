using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Explorer category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetExplorer() => new List<RegistryTweak>
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
        new()
        {
        Id = "explorer.show-status-bar",
        Title = "Show the status bar in File Explorer",
        Description = "Adds the bottom status bar (selection count, free space, etc.) to File Explorer " +
        "by setting ShowStatusBar=1. The default (0) hides it. Purely informational; no " +
        "behavioral change.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowStatusBar",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowStatusBar (1 = shown)",
        },
        new()
        {
        Id = "explorer.disable-sharing-wizard",
        Title = "Use classic file sharing instead of the wizard",
        Description = "Turns off the simplified 'Sharing Wizard' so the full advanced Sharing/security " +
        "dialog appears, by setting SharingWizardOn=0. The default (1) uses the consumer " +
        "wizard. Useful for precise NTFS/Share permission control.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "SharingWizardOn",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!SharingWizardOn (0 = classic)",
        },
        new()
        {
        Id = "explorer.item-checkboxes",
        Title = "Show checkboxes to select items",
        Description = "Shows selection checkboxes next to files/folders in Explorer so you can multi-select " +
        "by clicking them, by setting AutoCheckSelect=1. The default (0) hides the checkboxes.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "AutoCheckSelect",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!AutoCheckSelect (1 = on)",
        },
        new()
        {
        Id = "explorer.expand-nav-to-current",
        Title = "Expand navigation pane to current folder",
        Description = "Makes the File Explorer navigation pane automatically expand to reveal the folder " +
        "you are currently in, by setting NavPaneExpandToCurrentFolder=1. The default (0) keeps " +
        "the tree collapsed. Navigational convenience only.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "NavPaneExpandToCurrentFolder",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!NavPaneExpandToCurrentFolder (1 = on)",
        },
        new()
        {
        Id = "explorer.hide-frequent-folders",
        Title = "Hide frequent folders in Quick Access",
        Description = "Removes the 'Frequent folders' section from Quick Access by setting ShowFrequent=0. " +
        "The default (1) shows recently/frequently used folders there. Improves a clean " +
        "navigation pane.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowFrequent",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowFrequent (0 = hidden, 1 = shown)",
        },
        new()
        {
        Id = "explorer.hide-sync-notifications",
        Title = "Hide sync-provider notifications (OneDrive)",
        Description = "Stops File Explorer from showing 'sync provider' notifications (e.g. OneDrive sign-in " +
        "prompts) by setting ShowSyncProviderNotifications=0. The default (1) shows them. Removes " +
        "a common source of pop-ups in the navigation pane.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowSyncProviderNotifications",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowSyncProviderNotifications (0 = hidden)",
        },
        new()
        {
        Id = "explorer.separate-process",
        Title = "Open folder windows in separate processes",
        Description = "Runs each File Explorer window in its own explorer.exe process (SeparateProcess=1) instead of sharing one. If one window crashes it will not take the others down, at a small memory cost. The default (0) shares a single process.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "SeparateProcess",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!SeparateProcess (1 = separate)",
        },
        new()
        {
        Id = "explorer.show-menu-bar",
        Title = "Show the classic menu bar in File Explorer",
        Description = "Restores the traditional File/Edit/View menu bar in Explorer windows (AlwaysShowMenu=1). The default (0) hides it behind the ribbon/command bar. Purely a cosmetic preference.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "AlwaysShowMenu",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!AlwaysShowMenu (1 = shown)",
        },
        new()
        {
        Id = "explorer.hide-favorites",
        Title = "Hide the Favorites section in Quick Access",
        Description = "Removes the Favorites (pinned cloud and personal) section from the Quick Access pane in File Explorer (ShowFavorites=0, a Windows 11 setting). The default (1) shows it. Reduces clutter if you do not use it.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowFavorites",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowFavorites (0 = hidden)",
        },
        new()
        {
        Id = "explorer.disable-snap-assist",
        Title = "Disable Snap Assist",
        Description = "Turns off Snap Assist, the layout suggestions Windows shows after you snap a window to a screen edge (SnapAssist=0). The default (1) displays those arrangement hints. Purely a workflow preference; snapping a window still works without the suggestions.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "SnapAssist",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!SnapAssist (0 = off)",
        },
        new()
        {
        Id = "explorer.show-encrypted-files-green",
        Title = "Show encrypted files in green",
        Description = "Paints NTFS-encrypted files in green inside File Explorer (ShowEncrypt=1), making protected files easy to spot at a glance. The default (0) shows them in the normal color. Purely cosmetic and has no effect on the encryption itself.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowEncrypt",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowEncrypt (1 = green)",
        },
        new()
        {
        Id = "explorer.always-show-tray-icons",
        Title = "Always show all notification-area icons",
        Description = "Stops Windows from hiding inactive notification-area (system tray) icons behind the up-arrow overflow by setting EnableAutoTray=0, so every icon is always visible. The default (1) auto-hides idle icons. Convenience only; no functional change.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer",
        ValueName = "EnableAutoTray",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer!EnableAutoTray (0 = always show)",
        },
        new()
        {
        Id = "explorer.disable-thumbs-db",
        Title = "Disable Thumbs.db creation",
        Description = "Stops Windows from writing Thumbs.db thumbnail-cache files into folders (especially on network shares), by setting Explorer Advanced DisableThumbsDB=1. The default (0) creates them. Reduces stray files left behind on shared drives.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "DisableThumbsDB",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!DisableThumbsDB (1 = off)",
        },
        new()
        {
        Id = "explorer.show-compressed-files-blue",
        Title = "Show compressed files in blue",
        Description = "Paints NTFS-compressed files in a blue font inside File Explorer (ShowCompColor=1), mirroring the green used for encrypted files so you can spot compressed data at a glance. The default (0) shows them in the normal color. Cosmetic only.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowCompColor",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowCompColor (1 = blue)",
        },
        new()
        {
        Id = "explorer.disable-shortcut-tracking",
        Title = "Disable shortcut target tracking",
        Description = "Stops the NTFS distributed-link tracker from rewriting a shortcut's target when a file is moved or renamed on the same volume (Policies Explorer NoResolveTrack=1). The default (absent) lets shortcuts follow moved targets. Useful when you want shortcuts to keep their original path.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer",
        ValueName = "NoResolveTrack",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Policies\\Explorer!NoResolveTrack (1 = off)",
        },
        new()
        {
        Id = "explorer.show-all-folders-nav-pane",
        Title = "Show all folders in the navigation pane",
        Description = "Makes File Explorer's navigation pane list every folder (including This PC, Control Panel, and libraries) instead of just Quick Access items, by setting Explorer Advanced NavPaneShowAllFolders=1. The default (0) shows the simplified tree.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "NavPaneShowAllFolders",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!NavPaneShowAllFolders (1 = on)",
        },
        new()
        {
        Id = "explorer.disable-recent-docs-history",
        Title = "Clear recent docs on exit",
        Description = "Forces Windows to clear the Recent Documents history when you sign out, by setting Policies Explorer ClearRecentDocsOnExit=1. The default (absent) keeps the history. A privacy convenience for shared machines so the next user doesn't see your files.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer",
        ValueName = "ClearRecentDocsOnExit",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Policies\\Explorer!ClearRecentDocsOnExit (1 = on)",
        },
        new()
        {
        Id = "explorer.disable-new-app-prompt",
        Title = "Disable 'new app installed' notification",
        Description = "Stops Windows from popping the 'An app default was reset' / new-app-installed toast when a default-file-association app changes, by setting Explorer Advanced ShowNewAppSuggestedToast=0. The default (1) shows the suggestion. Reduces noise after updates.",
        Category = TweakCategory.Explorer,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "ShowNewAppSuggestedToast",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!ShowNewAppSuggestedToast (0 = off)",
        },
    };
}
