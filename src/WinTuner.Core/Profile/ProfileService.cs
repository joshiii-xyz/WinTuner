using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WinTuner.Core.Tweaks;

namespace WinTuner.Core.Profile;

/// <summary>
/// Captures the current live state of every tweak and re-applies a captured
/// configuration. Pure logic - no UI, fully unit-testable, so the headless
/// test suite can verify export/import without a window.
/// </summary>
public static class ProfileService
{
    /// <summary>Serializes the current state of each tweak to an indented JSON document.</summary>
    public static string Export(IEnumerable<RegistryTweak> tweaks, TweakEngine engine)
    {
        var snapshot = tweaks.ToDictionary(t => t.Id, t => engine.GetState(t).ToString());
        return JsonSerializer.Serialize(snapshot, new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>Parses a JSON profile document. Returns an empty map if the input is invalid.</summary>
    public static Dictionary<string, string> Parse(string json)
    {
        try
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            return dict ?? new Dictionary<string, string>();
        }
        catch (JsonException)
        {
            return new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Re-applies a captured profile: tweaks recorded as Enabled are applied,
    /// every other recorded tweak is reverted. Tweaks absent from the profile
    /// are left untouched. Safe to call repeatedly (idempotent).
    /// </summary>
    public static void Apply(IEnumerable<RegistryTweak> tweaks, Dictionary<string, string> states, TweakEngine engine)
    {
        foreach (var tweak in tweaks)
        {
            if (!states.TryGetValue(tweak.Id, out var state))
            {
                continue;
            }

            if (state == nameof(TweakState.Enabled))
            {
                engine.Apply(tweak);
            }
            else
            {
                engine.Revert(tweak);
            }
        }
    }
}
