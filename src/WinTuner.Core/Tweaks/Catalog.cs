using Microsoft.Win32;
using System.Linq;

namespace WinTuner.Core.Tweaks;

/// <summary>
/// The full catalog of known tweaks. Each category lives in its own Catalog.&lt;Category&gt;.cs
/// partial file (so the catalog scales to thousands of entries without one giant file) and is
/// concatenated here into Catalog.All. Every entry is declarative data - adding a tweak is just
/// a new entry in the right per-category file, no code changes anywhere else.
/// </summary>
public static partial class Catalog
{
    public static IReadOnlyList<RegistryTweak> All { get; } = GetExplorer()
        .Concat(GetPrivacy())
        .Concat(GetSystem())
        .Concat(GetPerformance())
        .Concat(GetAppearance())
        .Concat(GetSecurity())
        .Concat(GetNetwork())
        .Concat(GetGaming())
        .ToList();
}
