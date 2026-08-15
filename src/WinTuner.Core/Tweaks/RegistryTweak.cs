using Microsoft.Win32;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// A declarative, registry-backed tweak. Pure data: the engine interprets it,
/// so adding a tweak is just a new entry in the catalog - no new code path.
/// </summary>
public sealed record RegistryTweak
{
    /// <summary>Stable identifier, e.g. "explorer.show-file-extensions".</summary>
    public required string Id { get; init; }

    /// <summary>Human-readable title shown in the UI.</summary>
    public required string Title { get; init; }

    /// <summary>Thorough, factual description of what the tweak changes and its effect.</summary>
    public required string Description { get; init; }

    public required TweakCategory Category { get; init; }

    public required RegistryHive Hive { get; init; }

    public required string SubKey { get; init; }

    public required string ValueName { get; init; }

    public required RegistryValueKind ValueKind { get; init; }

    /// <summary>Value written when the tweak is applied (enabled).</summary>
    public required object EnabledValue { get; init; }

    /// <summary>Value written when the tweak is reverted (disabled).</summary>
    public required object DisabledValue { get; init; }

    /// <summary>The value the OS ships with by default, used by "Reset to default". Null means delete the value.</summary>
    public object? DefaultValue { get; init; }

    /// <summary>What the "value absent" condition means for this tweak.</summary>
    public TweakState AbsentState { get; init; } = TweakState.Unknown;

    /// <summary>True if applying requires writing to HKLM and therefore elevation.</summary>
    public bool RequiresElevation => Hive is RegistryHive.LocalMachine;

    /// <summary>Short, citable note on where this setting lives (for transparency).</summary>
    public string? Reference { get; init; }
}
