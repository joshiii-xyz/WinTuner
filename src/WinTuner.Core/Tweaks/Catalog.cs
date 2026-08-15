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
    };
}
