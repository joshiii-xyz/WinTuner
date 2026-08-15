using Microsoft.Win32;

namespace WinTuner.Core.Registry;

/// <summary>
/// In-memory registry for unit tests. Mirrors the subset of semantics the
/// engine relies on, including value-kind awareness for round-trip fidelity.
/// </summary>
public sealed class FakeRegistryProvider : IRegistryProvider
{
    private sealed record StoredValue(object Value, RegistryValueKind Kind);

    private readonly Dictionary<string, Dictionary<string, StoredValue>> _store = new(StringComparer.OrdinalIgnoreCase);

    private static string Key(RegistryHive hive, string subKey) => $"{hive}\\{subKey}";

    public object? GetValue(RegistryHive hive, string subKey, string valueName)
    {
        if (_store.TryGetValue(Key(hive, subKey), out var values) &&
            values.TryGetValue(valueName, out var stored))
        {
            return stored.Value;
        }
        return null;
    }

    public RegistryValueKind? GetValueKind(RegistryHive hive, string subKey, string valueName)
    {
        if (_store.TryGetValue(Key(hive, subKey), out var values) &&
            values.TryGetValue(valueName, out var stored))
        {
            return stored.Kind;
        }
        return null;
    }

    public void SetValue(RegistryHive hive, string subKey, string valueName, object value, RegistryValueKind kind)
    {
        var k = Key(hive, subKey);
        if (!_store.TryGetValue(k, out var values))
        {
            values = new Dictionary<string, StoredValue>(StringComparer.OrdinalIgnoreCase);
            _store[k] = values;
        }
        values[valueName] = new StoredValue(value, kind);
    }

    public void DeleteValue(RegistryHive hive, string subKey, string valueName)
    {
        if (_store.TryGetValue(Key(hive, subKey), out var values))
        {
            values.Remove(valueName);
        }
    }

    public bool KeyExists(RegistryHive hive, string subKey) =>
        _store.ContainsKey(Key(hive, subKey));

    public void DeleteKey(RegistryHive hive, string subKey) =>
        _store.Remove(Key(hive, subKey));
}
