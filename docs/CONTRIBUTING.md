# Contributing

WinTuner is data-driven: **adding a tweak is adding a record, not writing a feature.**
This document defines the rules so the catalog stays consistent, factual, and testable.

## Golden rules

1. **No fake entries.** Do not add a tweak you have not coded and verified. Do not add
   "coming soon" stubs to the UI or catalog.
2. **Facts only.** The description must state the concrete registry change and its
   observable effect. Do not claim unmeasured performance gains or make value judgments
   ("this is bad", "this is better") unless they are objectively true of the setting.
3. **Citable reference.** Every tweak has a `Reference` string pointing at the exact
   registry location (`HIVE\SubKey!ValueName`).
4. **Reversible.** Provide both `EnabledValue` and `DisabledValue`. Prefer a real
   `DefaultValue` (the OS default) so "Reset" restores stock behavior. If the OS default
   is "value absent", set `DefaultValue = null` (Reset will delete the value).
5. **Unique id.** `Id` is dotted, lowercase, namespaced by category
   (`explorer.show-file-extensions`). No duplicates (enforced by a test).
6. **Thorough description.** At least ~60 characters of real explanation (enforced by a
   test). Link the *what* to the *why* where helpful, but stay factual.

## How to add a tweak

1. Open `src/WinTuner.Core/Tweaks/Catalog.cs`.
2. Append a new `RegistryTweak` to the `All` list:

   ```csharp
   new()
   {
       Id = "privacy.disable-tailored-experiences",
       Title = "Disable tailored experiences with diagnostic data",
       Description = "Stops Windows from using your diagnostic data to personalize " +
                     "tips, ads, and recommendations in the OS. Setting the value to 0 " +
                     "disables the tailored-experiences feature. Requires sign-out/in " +
                     "to take full effect.",
       Category = TweakCategory.Privacy,
       Hive = RegistryHive.CurrentUser,
       SubKey = @"Software\Microsoft\Windows\CurrentVersion\Privacy",
       ValueName = "TailoredExperiencesWithDiagnosticDataEnabled",
       ValueKind = RegistryValueKind.DWord,
       EnabledValue = 0,
       DisabledValue = 1,
       DefaultValue = 1,
       AbsentState = TweakState.Disabled,
       Reference = "HKCU\\…\\Privacy!TailoredExperiencesWithDiagnosticDataEnabled",
   }
   ```

3. That's it. The UI lists it automatically under its category; the engine handles
   apply/revert/reset; the catalog-integrity tests re-run.

## When a tweak needs elevation

If the tweak writes to `HKLM`, set `Hive = RegistryHive.LocalMachine`. `RequiresElevation`
becomes `true` automatically, and the UI marks it "(requires administrator)". The app
currently applies it directly; an auto-relaunch-as-admin flow is on the roadmap
(see README).

## Testing your change

```pwsh
dotnet test src/WinTuner.Core.Tests/WinTuner.Core.Tests.csproj -c Release -p:Platform=x64
```

The suite includes:

- `Apply_WritesEnabledValue_AndReportsEnabled`
- `Revert_WritesDisabledValue_AndReportsDisabled`
- `Reset_DeletesValue_WhenDefaultIsNull`
- `Reset_WritesDefaultValue_WhenDefaultIsSet`
- `GetState_Absent_ReturnsDeclaredAbsentState`
- `Catalog_AllTweaks_HaveUniqueIds`
- `Catalog_AllTweaks_HaveThoroughDescriptions`
- `Catalog_HklmTweaks_RequireElevation`

When adding tweaks, keep these invariants green. Add a dedicated test if you introduce
new engine behavior.

## Code style

- `file_scoped` namespaces are fine; the `.Core` project uses `ImplicitUsings`.
- The `.App` project disables implicit usings (WinUI convention) — add explicit `using`s.
- Prefer `record`s for data, small focused classes for behavior.
- Keep `WinTuner.Core` free of any WinUI / `Microsoft.UI` references.
