using WinTuner.Core.Tweaks;

namespace WinTuner.App;

/// <summary>
/// Lightweight view-model for a single row in the System Scan view. Unlike
/// <see cref="TweakViewModel"/> (used by the per-category card list) this one
/// only needs to expose the live state and relay apply/revert - it does not
/// implement change notification because the scan list is rebuilt on demand.
/// </summary>
public sealed class ScanRowViewModel
{
    private readonly TweakEngine _engine;

    public ScanRowViewModel(RegistryTweak tweak, TweakEngine engine)
    {
        Tweak = tweak;
        _engine = engine;
        Refresh();
    }

    public RegistryTweak Tweak { get; }

    public string Title => Tweak.Title;
    public string Reference => Tweak.Reference ?? string.Empty;
    public string CategoryLabel =>
        Tweak.Category switch
        {
            TweakCategory.Explorer => "File Explorer",
            TweakCategory.Privacy => "Privacy",
            TweakCategory.Performance => "Performance",
            TweakCategory.Appearance => "Appearance",
            TweakCategory.System => "System",
            TweakCategory.Security => "Security",
            TweakCategory.Network => "Network",
            TweakCategory.Gaming => "Gaming",
            _ => Tweak.Category.ToString(),
        };

    public TweakState State { get; private set; }
    public bool IsOn => State == TweakState.Enabled;
    public string StateText => State.ToString();

    public void Refresh() => State = _engine.GetState(Tweak);
    public void Apply() => _engine.Apply(Tweak);
    public void Revert() => _engine.Revert(Tweak);
}
