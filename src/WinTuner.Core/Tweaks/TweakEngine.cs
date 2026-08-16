using Microsoft.Win32;
using WinTuner.Core.Registry;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Interprets <see cref="RegistryTweak"/> records against an <see cref="IRegistryProvider"/>.
/// All operations are synchronous against the registry; the engine carries no UI state.
///
/// Safety model: the first time a tweak is applied, the engine snapshots the value
/// it is about to overwrite (the "original") into memory. Reverting then restores that
/// real original rather than blindly writing the tweak's hardcoded opposite - so a tweak
/// applied over a non-default starting state can still be undone cleanly. The snapshot is
/// session-scoped: it is intentionally lost on restart, which is safe because after a reboot
/// the live registry already holds the applied (enabled) value and Revert writes the disabled
/// value as designed.
/// </summary>
public sealed class TweakEngine
{
    private readonly IRegistryProvider _registry;
    private readonly Dictionary<string, (object Value, RegistryValueKind Kind)> _backups = new();

    public TweakEngine(IRegistryProvider registry)
    {
        _registry = registry;
    }

    /// <summary>Applies (enables) the tweak, backing up the pre-existing value first.</summary>
    public void Apply(RegistryTweak tweak)
    {
        Backup(tweak);
        _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.EnabledValue, tweak.ValueKind);
    }

    /// <summary>Reverts (disables) the tweak, restoring the backed-up original when present.</summary>
    public void Revert(RegistryTweak tweak)
    {
        Backup(tweak);

        if (_backups.TryGetValue(tweak.Id, out var original))
        {
            // Restore the real value that existed before the first apply this session.
            _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, original.Value, original.Kind);
        }
        else
        {
            _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.DisabledValue, tweak.ValueKind);
        }
    }

    /// <summary>Resets the value to the OS default. If DefaultValue is null, the value is deleted.</summary>
    public void Reset(RegistryTweak tweak)
    {
        if (tweak.DefaultValue is null)
        {
            _registry.DeleteValue(tweak.Hive, tweak.SubKey, tweak.ValueName);
        }
        else
        {
            // Prefer the backed-up original if we have one; otherwise the declared default.
            if (_backups.TryGetValue(tweak.Id, out var original))
            {
                _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, original.Value, original.Kind);
            }
            else
            {
                _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.DefaultValue, tweak.ValueKind);
            }
        }
    }

    /// <summary>Reads the current state of the tweak relative to its enabled/disabled values.</summary>
    public TweakState GetState(RegistryTweak tweak)
    {
        var current = _registry.GetValue(tweak.Hive, tweak.SubKey, tweak.ValueName);
        if (current is null)
        {
            return tweak.AbsentState;
        }

        if (ValuesEqual(current, tweak.EnabledValue))
        {
            return TweakState.Enabled;
        }

        if (ValuesEqual(current, tweak.DisabledValue))
        {
            return TweakState.Disabled;
        }

        return TweakState.Unknown;
    }

    /// <summary>Reads the live state of every supplied tweak. Used by System Scan.</summary>
    public IReadOnlyDictionary<string, TweakState> ScanAll(IEnumerable<RegistryTweak> tweaks)
    {
        var result = new Dictionary<string, TweakState>();
        foreach (var tweak in tweaks)
        {
            result[tweak.Id] = GetState(tweak);
        }

        return result;
    }

    /// <summary>
    /// Captures the value currently in the registry (and its kind) before we modify it,
    /// but only once per session and only when the value actually differs from the
    /// enabled value (so we never record the already-applied value as the "original").
    /// </summary>
    private void Backup(RegistryTweak tweak)
    {
        if (_backups.ContainsKey(tweak.Id))
        {
            return;
        }

        var value = _registry.GetValue(tweak.Hive, tweak.SubKey, tweak.ValueName);
        var kind = _registry.GetValueKind(tweak.Hive, tweak.SubKey, tweak.ValueName);
        if (value is not null && kind is not null && !ValuesEqual(value, tweak.EnabledValue))
        {
            _backups[tweak.Id] = (value, kind.Value);
        }
    }

    private static bool ValuesEqual(object? a, object? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }
}
