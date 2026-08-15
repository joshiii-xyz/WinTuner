using Microsoft.Win32;
using WinTuner.Core.Registry;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// Interprets <see cref="RegistryTweak"/> records against an <see cref="IRegistryProvider"/>.
/// All operations are synchronous against the registry; the engine carries no UI state.
/// </summary>
public sealed class TweakEngine
{
    private readonly IRegistryProvider _registry;

    public TweakEngine(IRegistryProvider registry)
    {
        _registry = registry;
    }

    /// <summary>Applies (enables) the tweak.</summary>
    public void Apply(RegistryTweak tweak) =>
        _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.EnabledValue, tweak.ValueKind);

    /// <summary>Reverts (disables) the tweak.</summary>
    public void Revert(RegistryTweak tweak) =>
        _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.DisabledValue, tweak.ValueKind);

    /// <summary>Resets the value to the OS default. If DefaultValue is null, the value is deleted.</summary>
    public void Reset(RegistryTweak tweak)
    {
        if (tweak.DefaultValue is null)
        {
            _registry.DeleteValue(tweak.Hive, tweak.SubKey, tweak.ValueName);
        }
        else
        {
            _registry.SetValue(tweak.Hive, tweak.SubKey, tweak.ValueName, tweak.DefaultValue, tweak.ValueKind);
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

    /// <summary>
    /// Applies or reverts a tweak based on a desired state string ("Enabled"/"Disabled").
    /// Used by profile import so a captured configuration can be replayed.
    /// </summary>
    public void ApplyOrRevert(RegistryTweak tweak, IReadOnlyDictionary<string, string> states)
    {
        if (!states.TryGetValue(tweak.Id, out var state))
        {
            return;
        }

        if (state == nameof(TweakState.Enabled))
        {
            Apply(tweak);
        }
        else
        {
            Revert(tweak);
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
