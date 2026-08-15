using Microsoft.Win32;

namespace WinTuner.Core.Registry;

/// <summary>
/// Real registry access backed by Microsoft.Win32.Registry.
/// Operations that write to HKLM require the process to run elevated.
/// </summary>
public sealed class RealRegistryProvider : IRegistryProvider
{
    public object? GetValue(RegistryHive hive, string subKey, string valueName)
    {
        using var key = RegistryKey.OpenBaseKey(hive, RegistryView.Default).OpenSubKey(subKey);
        return key?.GetValue(valueName);
    }

    public void SetValue(RegistryHive hive, string subKey, string valueName, object value, RegistryValueKind kind)
    {
        using var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);
        using var key = baseKey.CreateSubKey(subKey, writable: true)
            ?? throw new InvalidOperationException($"Unable to open or create {hive}\\{subKey} for writing.");
        key.SetValue(valueName, value, kind);
    }

    public void DeleteValue(RegistryHive hive, string subKey, string valueName)
    {
        using var key = RegistryKey.OpenBaseKey(hive, RegistryView.Default).OpenSubKey(subKey, writable: true);
        key?.DeleteValue(valueName, throwOnMissingValue: false);
    }

    public bool KeyExists(RegistryHive hive, string subKey)
    {
        using var key = RegistryKey.OpenBaseKey(hive, RegistryView.Default).OpenSubKey(subKey);
        return key is not null;
    }

    public void DeleteKey(RegistryHive hive, string subKey)
    {
        using var baseKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default);
        baseKey.DeleteSubKeyTree(subKey, throwOnMissingSubKey: false);
    }
}
