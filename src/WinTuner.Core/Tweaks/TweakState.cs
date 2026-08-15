namespace WinTuner.Core.Tweaks;

/// <summary>Whether a tweak is currently active on the machine.</summary>
public enum TweakState
{
    /// <summary>The registry value matches the tweak's enabled state.</summary>
    Enabled,

    /// <summary>The registry value matches the tweak's disabled (reverted) state.</summary>
    Disabled,

    /// <summary>The current value is neither enabled nor disabled (e.g. absent or a third state).</summary>
    Unknown,
}
