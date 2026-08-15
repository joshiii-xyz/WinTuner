using Microsoft.Win32;

namespace WinTuner.Core.Registry;

/// <summary>
/// Abstraction over the Windows registry so tweak logic can be unit-tested
/// against an in-memory fake instead of mutating the real machine.
/// </summary>
public interface IRegistryProvider
{
    /// <summary>Reads a value. Returns null if the key or value does not exist.</summary>
    object? GetValue(RegistryHive hive, string subKey, string valueName);

    /// <summary>Reads the value kind of an existing value, or null if it does not exist.</summary>
    RegistryValueKind? GetValueKind(RegistryHive hive, string subKey, string valueName);

    /// <summary>Writes a value, creating the key path if necessary.</summary>
    void SetValue(RegistryHive hive, string subKey, string valueName, object value, RegistryValueKind kind);

    /// <summary>Deletes a single value. No-op if absent.</summary>
    void DeleteValue(RegistryHive hive, string subKey, string valueName);

    /// <summary>True if the key exists.</summary>
    bool KeyExists(RegistryHive hive, string subKey);

    /// <summary>Deletes a key and all its subkeys. No-op if absent.</summary>
    void DeleteKey(RegistryHive hive, string subKey);
}
