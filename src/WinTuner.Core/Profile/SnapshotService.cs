using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WinTuner.Core.Tweaks;

namespace WinTuner.Core.Profile;

/// <summary>
/// Captures the *raw* registry state of every known tweak to a JSON file and
/// restores it later. Unlike the engine's in-memory session backup (lost on
/// reboot), a snapshot is durable: it lets a user fully undo a set of tweaks
/// applied in a previous session - including restoring a value that did not
/// exist before (the entry is deleted on restore). Pure logic, no UI, so the
/// headless test suite verifies capture/restore without a window.
/// </summary>
public static class SnapshotService
{
    public sealed record Entry(string Hive, string SubKey, string ValueName, string? Value, string Kind);

    /// <summary>Builds a serializable snapshot of the current raw state of every tweak.</summary>
    public static Dictionary<string, Entry> Capture(IEnumerable<RegistryTweak> tweaks, TweakEngine engine)
    {
        var result = new Dictionary<string, Entry>();
        foreach (var tweak in tweaks)
        {
            var (value, kind) = engine.CaptureRaw(tweak);
            result[tweak.Id] = new Entry(
                tweak.Hive.ToString(),
                tweak.SubKey,
                tweak.ValueName,
                value,
                kind);
        }

        return result;
    }

    /// <summary>Serializes a snapshot to an indented JSON document.</summary>
    public static string Export(IEnumerable<RegistryTweak> tweaks, TweakEngine engine)
    {
        return JsonSerializer.Serialize(Capture(tweaks, engine), new JsonSerializerOptions { WriteIndented = true });
    }

    /// <summary>
    /// Parses a snapshot document. Returns null when the input is not a valid
    /// snapshot, so callers can surface a clear error instead of silently doing nothing.
    /// </summary>
    public static Dictionary<string, Entry>? Parse(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, Entry>>(json);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    /// <summary>
    /// Restores every entry in the snapshot, matching by tweak Id. Restores the
    /// original value (or deletes it when the snapshot recorded no value). Tweaks
    /// whose Id is no longer in the catalog are skipped.
    /// </summary>
    public static void Restore(
        IEnumerable<RegistryTweak> tweaks,
        Dictionary<string, Entry> snapshot,
        TweakEngine engine)
    {
        var byId = tweaks.ToDictionary(t => t.Id);
        foreach (var (id, entry) in snapshot)
        {
            if (!byId.TryGetValue(id, out var tweak))
            {
                continue;
            }

            engine.WriteValue(tweak, entry.Value, entry.Kind);
        }
    }
}
