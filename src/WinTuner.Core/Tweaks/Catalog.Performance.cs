using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Performance category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetPerformance() => new List<RegistryTweak>
    {
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
        RequiresReboot = true,
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
        RequiresReboot = true,
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
        new()
        {
        Id = "performance.disable-ndu",
        Title = "Disable NDU (network data usage) service",
        Description = "Sets the Ndu service Start value to 4 (disabled). Ndu monitors per-process network " +
        "usage; disabling it can slightly improve network throughput on some systems but " +
        "removes data-usage stats. REQUIRES ELEVATION and a reboot. Reset deletes the value.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\Ndu",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 2,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\Ndu!Start (4 = disabled)",
        },
        new()
        {
        Id = "performance.prioritize-foreground",
        Title = "Prioritize foreground apps (CPU)",
        Description = "Biases the scheduler toward the foreground app by setting PriorityControl " +
        "Win32PrioritySeparation to 38 (hex 0x26). The default (2) is balanced. Can make the " +
        "active window feel more responsive at the cost of background tasks. Requires elevation.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\PriorityControl",
        ValueName = "Win32PrioritySeparation",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 38,
        DisabledValue = 2,
        DefaultValue = 2,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\PriorityControl!Win32PrioritySeparation (38 = foreground)",
        },
        new()
        {
        Id = "performance.disable-prefetcher",
        Title = "Disable Prefetcher",
        Description = "Sets the OS Prefetcher (which pre-loads commonly used files at boot) to off by setting " +
        "PrefetchParameters EnablePrefetcher=0. Can help on SSD-only systems where prefetching adds " +
        "little. REQUIRES ELEVATION and a reboot to take effect.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management\PrefetchParameters",
        ValueName = "EnablePrefetcher",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 3,
        DefaultValue = 3,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\PrefetchParameters!EnablePrefetcher (0 = off)",
        },
        new()
        {
        Id = "performance.disable-paging-executive",
        Title = "Keep executive in RAM (no paging)",
        Description = "Prevents the kernel/executive from being paged to disk by setting Memory Management " +
        "DisablePagingExecutive=1, keeping core OS code in physical RAM. Helps systems with ample " +
        "RAM. REQUIRES ELEVATION and a reboot.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management",
        ValueName = "DisablePagingExecutive",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Memory Management!DisablePagingExecutive (1 = keep in RAM)",
        },
        new()
        {
        Id = "performance.disable-last-access-timestamp",
        Title = "Disable NTFS last-access timestamps",
        Description = "Stops NTFS from updating the last-access time on every file read (NtfsDisableLastAccessUpdate=1), " +
        "reducing disk writes on busy volumes. The default (0) keeps the stamp updated. Harmless on " +
        "modern SSDs but can matter on high-IOPS workloads.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\FileSystem",
        ValueName = "NtfsDisableLastAccessUpdate",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\FileSystem!NtfsDisableLastAccessUpdate (1 = off)",
        },
        new()
        {
        Id = "performance.disable-8dot3-names",
        Title = "Disable 8.3 filename creation",
        Description = "Stops NTFS from generating legacy '8.3' (DOS-style, e.g. PROGRA~1) filenames for new files " +
        "(NtfsDisable8dot3NameCreation=1), slightly reducing metadata overhead. The default (0) keeps " +
        "8.3 names for compatibility with old software.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\FileSystem",
        ValueName = "NtfsDisable8dot3NameCreation",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\FileSystem!NtfsDisable8dot3NameCreation (1 = off)",
        },
        new()
        {
        Id = "performance.disable-search-service",
        Title = "Disable the Windows Search indexing service",
        Description = "Sets the Windows Search service (WSearch) Start value to 4 (disabled), stopping background file indexing. This can cut disk/CPU usage but breaks Start/menu search and index-dependent features until re-enabled. REQUIRES ELEVATION and a reboot. Reset restores the default service start.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\WSearch",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 3,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\WSearch!Start (4 = disabled)",
        },
        new()
        {
        Id = "performance.disable-system-restore",
        Title = "Disable System Restore",
        Description = "Turns off System Restore system-wide by setting SystemRestore DisableSR=1, stopping the creation of restore points and freeing the disk space they consume. The default (0) keeps restore points. Requires elevation; you lose the ability to roll back system changes.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore",
        ValueName = "DisableSR",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\SystemRestore!DisableSR (1 = off)",
        },
        new()
        {
        Id = "performance.enable-large-system-cache",
        Title = "Enable large system cache (file server)",
        Description = "Biases the memory manager to keep more file cache and less per-process working set by setting Memory Management LargeSystemCache=1, which can speed up file serving and large copies. The default (0) balances the two. REQUIRES ELEVATION and a reboot; on small-RAM desktops it can hurt interactive performance.",
        Category = TweakCategory.Performance,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management",
        ValueName = "LargeSystemCache",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Memory Management!LargeSystemCache (1 = on)",
        },
    };
}
