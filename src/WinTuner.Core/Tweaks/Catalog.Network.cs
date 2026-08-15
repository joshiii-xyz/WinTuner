using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Tweak catalog for the Network category. One file per category keeps the catalog organized and
/// future-proof as it grows toward thousands of entries - each category is edited independently
/// and every entry stays in its own file. All entries are declarative data; descriptions state
/// exactly which registry value changes. An entry's Id prefix always matches its Category.
/// New tweaks are added here only - no code changes.
/// </summary>
public static partial class Catalog
{
    public static List<RegistryTweak> GetNetwork() => new List<RegistryTweak>
    {
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
        RequiresReboot = true,
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
        RequiresReboot = true,
        Reference = "HKLM\\...\\LanmanServer\\Parameters!SMB1 (0 = disabled)",
        },
        new()
        {
        Id = "network.disable-network-throttling",
        Title = "Disable multimedia network throttling",
        Description = "Stops Windows from throttling network throughput for 'multimedia' tuning by setting " +
        "Multimedia SystemProfile NetworkThrottlingIndex to 0xFFFFFFFF (-1, disabled). The " +
        "default (10) caps throughput. Requires elevation; can improve LAN transfer speeds.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile",
        ValueName = "NetworkThrottlingIndex",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = -1,
        DisabledValue = 10,
        DefaultValue = 10,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\SystemProfile!NetworkThrottlingIndex (0xFFFFFFFF = off)",
        },
        new()
        {
        Id = "network.disable-ncsi-probing",
        Title = "Disable NCSI active Internet probing",
        Description = "Stops the Network Connectivity Status Indicator from actively probing Microsoft's " +
        "servers to decide if the Internet is reachable (NlaSvc Internet EnableActiveProbing=0). " +
        "The default (1) probes. Requires elevation; the tray may report limited connectivity " +
        "even when online.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\NlaSvc\Parameters\Internet",
        ValueName = "EnableActiveProbing",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKLM\\...\\NlaSvc\\Parameters\\Internet!EnableActiveProbing (0 = off)",
        },
        new()
        {
        Id = "network.disable-ics",
        Title = "Disable Internet Connection Sharing",
        Description = "Stops the Internet Connection Sharing service (SharedAccess) by setting its Start value to " +
        "4 (disabled). ICS lets the PC act as a router/NAT for other devices; most home users do " +
        "not need it and it expands the network attack surface. REQUIRES ELEVATION and a reboot. " +
        "Reset deletes the value to restore the default service start.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\SharedAccess",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 2,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\SharedAccess!Start (4 = disabled)",
        },
        new()
        {
        Id = "network.disable-proxy-autodetect",
        Title = "Disable proxy auto-detection (WPAD)",
        Description = "Turns off automatic proxy discovery (WPAD) by setting Internet Settings AutoDetect=0. The " +
        "default (1) lets Windows probe for a proxy configuration. Disabling avoids a legacy " +
        "spoofing path on untrusted networks.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.CurrentUser,
        SubKey = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
        ValueName = "AutoDetect",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 0,
        DisabledValue = 1,
        DefaultValue = 1,
        AbsentState = TweakState.Disabled,
        Reference = "HKCU\\...\\Internet Settings!AutoDetect (0 = off)",
        },
        new()
        {
        Id = "network.disable-lltdio",
        Title = "Disable Link-Layer Topology Discovery I/O",
        Description = "Sets the Link-Layer Topology Discovery Mapper I/O service (lltdsvc) Start value to 4 (disabled), turning off network map discovery. Reduces network chatter and attack surface on machines that do not need the Network Map feature. REQUIRES ELEVATION and a reboot. Reset restores the default service start.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\lltdsvc",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 3,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\lltdsvc!Start (4 = disabled)",
        },
        new()
        {
        Id = "network.disable-function-discovery",
        Title = "Disable Function Discovery resource publication",
        Description = "Sets the Function Discovery Resource Publication service (FDResPub) Start value to 4 (disabled), stopping the machine from publishing itself for network discovery. Reduces attack surface on machines that do not need to be discovered on the LAN. REQUIRES ELEVATION and a reboot. Reset restores the default service start.",
        Category = TweakCategory.Network,
        Hive = RegistryHive.LocalMachine,
        SubKey = @"SYSTEM\CurrentControlSet\Services\FDResPub",
        ValueName = "Start",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 4,
        DisabledValue = 3,
        DefaultValue = null,
        AbsentState = TweakState.Disabled,
        RequiresReboot = true,
        Reference = "HKLM\\...\\Services\\FDResPub!Start (4 = disabled)",
        },
    };
}
