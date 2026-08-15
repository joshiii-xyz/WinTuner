using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using WinTuner.Core.Tweaks;

namespace WinTuner.App;

/// <summary>
/// Presentation wrapper around a <see cref="RegistryTweak"/>. Holds observable
/// UI state (toggle, computed state text, elevation visibility) and relays
/// apply/revert/reset calls to the engine. Keeps the XAML data-bound and clean.
/// </summary>
public sealed class TweakViewModel : INotifyPropertyChanged
{
    private readonly TweakEngine _engine;
    private bool _isOn;
    private string _stateText = string.Empty;

    public TweakViewModel(RegistryTweak tweak, TweakEngine engine)
    {
        Tweak = tweak;
        _engine = engine;
        Refresh();
    }

    public RegistryTweak Tweak { get; }

    public string Title => Tweak.Title;
    public string Description => Tweak.Description;
    public string Reference => Tweak.Reference ?? string.Empty;
    public bool RequiresElevation => Tweak.RequiresElevation;

    public bool IsOn
    {
        get => _isOn;
        set
        {
            if (_isOn != value)
            {
                _isOn = value;
                OnPropertyChanged();
            }
        }
    }

    public string StateText
    {
        get => _stateText;
        private set
        {
            if (_stateText != value)
            {
                _stateText = value;
                OnPropertyChanged();
            }
        }
    }

    public Visibility ElevationVisibility =>
        RequiresElevation ? Visibility.Visible : Visibility.Collapsed;

    public void Refresh()
    {
        var state = _engine.GetState(Tweak);
        IsOn = state == TweakState.Enabled;
        StateText = state.ToString();
    }

    public void Apply() => _engine.Apply(Tweak);
    public void Revert() => _engine.Revert(Tweak);
    public void Reset() => _engine.Reset(Tweak);

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
