using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Privacy category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetPrivacy() => new List<RegistryTweak>
    {
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
        new()
        {
        Id = "privacy.disable-activity-history",
        Title = "Disable activity history (Timeline)",
        Description = "Stops Windows from collecting and uploading your activity history used for Timeline " +
        "and cross-device resume, by setting ActivityPicker EnableActivityFeed=0. The default " +
        "(1) collects it. A sign-out/in may be required to fully stop upload.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\ActivityPicker",
        ValueName = "EnableActivityFeed",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\ActivityPicker!EnableActivityFeed (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-location",
        Title = "Disable location services",
        Description = "Turns off the OS location service (used by maps, weather, and some apps) by setting " +
        "Sensor Permissions Location EnableLocation=0. The default (1) allows location access. " +
        "Apps that need location will no longer receive it.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows NT\CurrentVersion\Sensor Permissions\Location",
        ValueName = "EnableLocation",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Sensor Permissions\\Location!EnableLocation (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-error-reporting",
        Title = "Disable Windows Error Reporting uploads",
        Description = "Stops Windows from sending crash/error reports to Microsoft by setting Windows Error " +
        "Reporting Disabled=1. The default (absent) allows reporting. Requires elevation; local " +
        "crash logs still accrue, only the upload is stopped.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows\Windows Error Reporting",
        ValueName = "Disabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Windows Error Reporting!Disabled (1 = off)",
        },
        new()
        {
        Id = "privacy.disable-settings-suggestions",
        Title = "Disable suggested content in Settings",
        Description = "Removes Microsoft's suggested apps and content rows inside the Settings app " +
        "(ContentDeliveryManager SubscribedContent-338893=0). The default (1) shows them. " +
        "Reduces promotional noise in Settings.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
        ValueName = "SubscribedContent-338893",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\ContentDeliveryManager!SubscribedContent-338893 (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-clipboard-history",
        Title = "Disable clipboard history",
        Description = "Turns off the local clipboard history (Win+V) by setting Clipboard " +
        "EnableClipboardHistory=0. The default (1) keeps a rolling history of copied items. " +
        "Disabling also clears the stored history.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Clipboard",
        ValueName = "EnableClipboardHistory",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Clipboard!EnableClipboardHistory (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-cloud-clipboard",
        Title = "Disable clipboard cloud sync",
        Description = "Stops clipboard items from syncing across your devices (the cloud clipboard) by setting " +
        "Clipboard EnableCloudClipboard=0. The default (1) syncs copied content to your account. " +
        "Local clipboard still works.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Clipboard",
        ValueName = "EnableCloudClipboard",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Clipboard!EnableCloudClipboard (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-telemetry",
        Title = "Set telemetry level to Security (minimum)",
        Description = "Forces Windows telemetry to the lowest level via the policy AllowTelemetry=0 (Security " +
        "only). The default (absent/3) sends enhanced diagnostic data. NOTE: on non-Enterprise " +
        "editions Windows may still send a baseline; this sets the strongest available policy. " +
        "Requires elevation.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows\DataCollection",
        ValueName = "AllowTelemetry",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 3,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\DataCollection!AllowTelemetry (policy; 0 = Security)",
        },
        new()
        {
        Id = "privacy.disable-app-launch-tracking",
        Title = "Disable app-launch tracking for Start recommendations",
        Description = "Stops Windows from tracking which apps you launch to personalize Start menu and search results (Start_TrackProgs=0). The default (1) tracks launches. Helps privacy at the cost of less personalized Start suggestions.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "Start_TrackProgs",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!Start_TrackProgs (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-recent-docs",
        Title = "Disable recent items in Jump Lists and Start",
        Description = "Stops Windows from remembering recently opened documents for Jump Lists and the Start menu (Start_TrackDocs=0). The default (1) keeps a recent-items history. Improves privacy on shared machines.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
        ValueName = "Start_TrackDocs",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Explorer\\Advanced!Start_TrackDocs (0 = off)",
        },
        new()
        {
        Id = "privacy.disable-diagnostics-tracking-service",
        Title = "Disable the Diagnostics & Telemetry service",
        Description = "Sets the Connected User Experiences and Telemetry service (DiagTrack) Start value to 4 (disabled), stopping background diagnostic-data collection and upload. REQUIRES ELEVATION and a reboot to take effect. The default (2) keeps it running.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\DiagTrack",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 2,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\DiagTrack!Start (4 = disabled)",
        },
        new()
        {
        Id = "privacy.disable-cloud-search-in-start",
        Title = "Disable cloud content in Start search",
        Description = "Removes cloud, OneDrive, and web-connected results from the Start menu search by setting CloudSearchEnabled=0. The default (1) blends cloud content into local Start search. Keeps search local and reduces calls to Microsoft services.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Search",
        ValueName = "CloudSearchEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Search!CloudSearchEnabled (0 = local only)",
        },
        new()
        {
        Id = "privacy.disable-lockscreen-tips",
        Title = "Disable lock screen tips and suggestions",
        Description = "Stops Windows from showing tips, fun facts, and promotional content on the lock screen by setting ContentDeliveryManager SubscribedContent-338387=0. The default (1) rotates that content on the lock screen. Reduces unsolicited lock-screen noise.",
        Category = TweakCategory.Privacy,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
        ValueName = "SubscribedContent-338387",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\ContentDeliveryManager!SubscribedContent-338387 (0 = off)",
        },
    };
}
