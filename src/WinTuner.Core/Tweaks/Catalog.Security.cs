using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Security category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetSecurity() => new List<RegistryTweak>
    {
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
        RequiresReboot = true,
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
        RequiresReboot = true,
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
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\upnphost!Start (4 = disabled)",
        },
        new()
        {
        Id = "security.disable-llmnr",
        Title = "Disable LLMNR name resolution",
        Description = "Turns off Link-Local Multicast Name Resolution (Dnscache Parameters EnableLLMNR=0), a " +
        "legacy protocol that can be abused for local network spoofing. The default (1) enables " +
        "it. Requires elevation; DNS and NetBIOS (if enabled) still resolve names.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\Dnscache\Parameters",
        ValueName = "EnableLLMNR",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Dnscache\\Parameters!EnableLLMNR (0 = off)",
        },
        new()
        {
        Id = "security.enable-dep-all",
        Title = "Enable DEP for all processes",
        Description = "Sets Data Execution Prevention to 'Always On' (Memory Management NoExecute=3) so all " +
        "processes are protected against code execution in data memory. The default is OptOut (2). " +
        "REQUIRES ELEVATION and a reboot; very old software may be incompatible.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Memory Management",
        ValueName = "NoExecute",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 3,
        DisabledValue = 2,
        DefaultValue = 2,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Memory Management!NoExecute (3 = Always On)",
        },
        new()
        {
        Id = "security.disable-remote-assistance",
        Title = "Disable Remote Assistance",
        Description = "Prevents others from offering/requesting Remote Assistance to this PC by setting " +
        "Remote Assistance fAllowToGetHelp=0. The default (1) allows it. Requires elevation; " +
        "this is distinct from the separate Remote Desktop feature.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Remote Assistance",
        ValueName = "fAllowToGetHelp",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Remote Assistance!fAllowToGetHelp (0 = disabled)",
        },
        new()
        {
        Id = "security.restrict-anonymous",
        Title = "Restrict anonymous SID/name enumeration",
        Description = "Blocks anonymous users from enumerating account names/SIDs via Lsa RestrictAnonymous=1, " +
        "closing a reconnaissance path on untrusted networks. The default (0) is permissive. " +
        "Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
        ValueName = "RestrictAnonymous",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Lsa!RestrictAnonymous (1 = restricted)",
        },
        new()
        {
        Id = "security.disable-remote-desktop",
        Title = "Disable Remote Desktop (incoming)",
        Description = "Blocks incoming Remote Desktop connections by setting Terminal Server " +
        "fDenyTSConnections=1. The default (0) allows RDP. Useful on machines that never need " +
        "remote access. REQUIRES ELEVATION and a reboot (or TermService restart) to fully take " +
        "effect.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Terminal Server",
        ValueName = "fDenyTSConnections",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Terminal Server!fDenyTSConnections (1 = denied)",
        },
        new()
        {
        Id = "security.require-ctrl-alt-del",
        Title = "Require Ctrl+Alt+Delete at sign-in",
        Description = "Forces the secure Ctrl+Alt+Delete gesture before the sign-in prompt by setting Winlogon " +
        "DisableCAD=0. The default (1) allows signing in without it. Adds a phishing-resistant step. " +
        "Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon",
        ValueName = "DisableCAD",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Winlogon!DisableCAD (0 = required)",
        },
        new()
        {
        Id = "security.disable-script-host",
        Title = "Disable Windows Script Host",
        Description = "Disables the Windows Script Host (wscript/cscript) so .vbs/.js files no longer run, " +
        "blocking a common malware delivery path, by setting Script Host Settings Enabled=0. The " +
        "default (1) allows scripts. Requires elevation; legitimate scripts (rare) will stop.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows Script Host\Settings",
        ValueName = "Enabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Windows Script Host\\Settings!Enabled (0 = off)",
        },
        new()
        {
        Id = "security.disable-rdp-password-saving",
        Title = "Disable saving RDP credentials",
        Description = "Prevents the Remote Desktop client from storing passwords locally via the policy " +
        "Terminal Services DisablePasswordSaving=1. The default (absent) allows saving credentials. " +
        "Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services",
        ValueName = "DisablePasswordSaving",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Terminal Services!DisablePasswordSaving (policy; 1 = off)",
        },
        new()
        {
        Id = "security.enable-uac-secure-desktop",
        Title = "UAC prompts on secure desktop",
        Description = "Makes User Account Control elevation prompts appear on the secure (dimmed) desktop by " +
        "setting Policies\\System PromptOnSecureDesktop=1, so malicious windows can't spoof the " +
        "prompt. The default (0) shows prompts on the interactive desktop. Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
        ValueName = "PromptOnSecureDesktop",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Policies\\System!PromptOnSecureDesktop (1 = secure)",
        },
        new()
        {
        Id = "security.disable-ssdp-discovery",
        Title = "Disable SSDP Discovery service",
        Description = "Sets the SSDP Discovery service (SSDPSRV) Start value to 4 (disabled), turning off discovery of UPnP devices on the network. Reduces attack surface on machines that do not use UPnP discovery. REQUIRES ELEVATION and a reboot. Reset restores the default service start.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\SSDPSRV",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 3,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\SSDPSRV!Start (4 = disabled)",
        },
        new()
        {
        Id = "security.restrict-anonymous-sam",
        Title = "Restrict anonymous SAM enumeration",
        Description = "Sets RestrictAnonymousSam=1 so anonymous users cannot enumerate the Security Accounts Manager (SAM) database. The default (0) allows it. A cheap hardening step that requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
        ValueName = "RestrictAnonymousSam",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Control\\Lsa!RestrictAnonymousSam (1 = restricted)",
        },
        new()
        {
        Id = "security.enable-sehop",
        Title = "Enable Structured Exception Handler Overwrite Protection",
        Description = "Enables SEHOP (DisableExceptionChainValidation=0), a process-level mitigation that blocks exceptions from being hijacked. The default (0) already enables it; setting it to 1 disables the protection. Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\kernel",
        ValueName = "DisableExceptionChainValidation",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\kernel!DisableExceptionChainValidation (0 = SEHOP on)",
        },
        new()
        {
        Id = "security.hide-last-user",
        Title = "Hide the last signed-in user on the logon screen",
        Description = "Sets the policy DontDisplayLastUserName=1 so the lock/logon screen no longer shows the previous user's name and avatar. The default (absent) shows the last user. Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
        ValueName = "DontDisplayLastUserName",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Policies\\System!DontDisplayLastUserName (policy)",
        },
        new()
        {
        Id = "security.disable-print-spooler",
        Title = "Disable Print Spooler service",
        Description = "Sets the Print Spooler service (Spooler) Start value to 4 (disabled), removing local print rendering and the PrintNightmare attack surface for machines that never print. REQUIRES ELEVATION and a reboot. Reset deletes the value to restore the default service start.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\Spooler",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 2,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\Spooler!Start (4 = disabled)",
        },
        new()
        {
        Id = "security.disable-wdigest-plaintext",
        Title = "Disable WDigest plaintext credential caching",
        Description = "Stops the WDigest provider from caching logon credentials in plaintext memory (UseLogonCredential=0), closing a pass-the-hash exposure on the local machine. The default (0) is already secure; this enforces it. REQUIRES ELEVATION and a reboot or sign-out to take full effect.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest",
        ValueName = "UseLogonCredential",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\WDigest!UseLogonCredential (0 = secure)",
        },
        new()
        {
        Id = "security.restrict-null-session",
        Title = "Restrict null-session access",
        Description = "Blocks anonymous (null-session) clients from accessing named pipes and shares by setting Lsa RestrictNullSessAccess=1, reducing reconnaissance surface on untrusted networks. The default (1) already restricts it; this enforces the setting. Requires elevation.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
        ValueName = "RestrictNullSessAccess",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\Lsa!RestrictNullSessAccess (1 = restricted)",
        },
        new()
        {
        Id = "security.enforce-ntlmv2-only",
        Title = "Enforce NTLMv2 only (no LM/NTLMv1)",
        Description = "Sets the LAN Manager authentication level to 5 (Lsa LmCompatibilityLevel=5) so the machine only sends and accepts NTLMv2, refusing the broken LM and NTLMv1 protocols. The default (3) still accepts older protocols. REQUIRES ELEVATION and a reboot.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
        ValueName = "LmCompatibilityLevel",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 5,
        DisabledValue = 3,
        DefaultValue = 3,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Lsa!LmCompatibilityLevel (5 = NTLMv2 only)",
        },
        new()
        {
        Id = "security.enable-fips-mode",
        Title = "Enable FIPS-compliant cryptography",
        Description = "Forces the system to use only FIPS 140-validated cryptographic algorithms (Lsa FIPSAlgorithmPolicy Enabled=1), as required in some government/compliance environments. The default (0) allows the broader algorithm set. REQUIRES ELEVATION and a reboot; some apps break under FIPS.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy",
        ValueName = "Enabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\FIPSAlgorithmPolicy!Enabled (1 = FIPS on)",
        },
        new()
        {
        Id = "security.disable-rpc-print-remote",
        Title = "Restrict remote RPC to Print Spooler",
        Description = "Hardens the Print Spooler by requiring administrator approval for remote RPC connections (RpcAuthnLevelPrivacyEnabled=1), mitigating PrintNightmare-style remote code execution. The default (absent) is less strict. REQUIRES ELEVATION and a reboot.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Print",
        ValueName = "RpcAuthnLevelPrivacyEnabled",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Print!RpcAuthnLevelPrivacyEnabled (1 = strict)",
        },
        new()
        {
        Id = "security.disable-lmhash-storage",
        Title = "Prevent LM hash storage",
        Description = "Stops Windows from storing the weak LAN Manager (LM) password hash in the SAM database (SecurityProviders NoLMHash=1), so only the stronger NTLM hash is kept. The default (absent) may store LM hashes. REQUIRES ELEVATION and a reboot to fully apply.",
        Category = TweakCategory.Security,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Control\Lsa",
        ValueName = "NoLMHash",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Lsa!NoLMHash (1 = off)",
        },
    };
}
