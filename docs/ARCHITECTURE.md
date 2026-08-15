# Architecture deep-dive

This document explains the internal design of `WinTuner.Core` and how the WinUI 3
shell consumes it. It is intended for contributors who will add tweaks or extend the
engine.

## 1. The data model

### `RegistryTweak` (record)

A single immutable description of one registry-backed setting. It is a C# `record`, so
it is value-equatable and easy to clone with `with`. Required fields:

| Field          | Meaning                                                              |
|----------------|---------------------------------------------------------------------|
| `Id`           | Stable, dotted, unique key, e.g. `explorer.show-file-extensions`.   |
| `Title`        | Short UI label.                                                     |
| `Description`  | Thorough, factual prose: what registry value changes and its effect.|
| `Category`     | One of `TweakCategory`.                                             |
| `Hive`         | `RegistryHive` (e.g. `CurrentUser`, `LocalMachine`).                |
| `SubKey`       | Registry path under the hive.                                       |
| `ValueName`    | The value name.                                                     |
| `ValueKind`    | `RegistryValueKind` (DWord, String, …).                             |
| `EnabledValue` | Written when the user clicks **Apply**.                             |
| `DisabledValue`| Written when the user clicks **Revert**.                            |
| `DefaultValue` | OS default; written by **Reset**. `null` ⇒ delete the value.        |
| `AbsentState`  | What "value absent" means (usually `Disabled`).                     |
| `Reference`    | Citable location, e.g. `HKCU\…\Advanced!HideFileExt`.               |

Derived: `RequiresElevation` is `true` whenever `Hive == LocalMachine`, because writing
to HKLM requires an elevated process.

### `TweakState` (enum)

`Enabled`, `Disabled`, or `Unknown`. `Unknown` covers any value that is neither the
enabled nor disabled value (e.g. a third-party tool wrote something else, or the user
manually edited the key). The UI shows this so the user is never misled about the true
state.

### `TweakCategory` (enum)

The navigation taxonomy. See `CATEGORIES.md`.

## 2. The registry abstraction

`IRegistryProvider` is the seam that makes the engine testable:

```csharp
public interface IRegistryProvider
{
    object? GetValue(RegistryHive hive, string subKey, string valueName);
    void SetValue(RegistryHive hive, string subKey, string valueName, object value, RegistryValueKind kind);
    void DeleteValue(RegistryHive hive, string subKey, string valueName);
    bool KeyExists(RegistryHive hive, string subKey);
    void DeleteKey(RegistryHive hive, string subKey);
}
```

- **`RealRegistryProvider`** — backed by `Microsoft.Win32.Registry`. Used by the app.
  HKLM writes throw `InvalidOperationException` unless the process is elevated.
- **`FakeRegistryProvider`** — in-memory dictionary, value-kind aware. Used by tests so
  we never mutate the real machine and never depend on a Windows registry existing.

Because the engine depends only on the *interface*, tests run on any OS and any CI
runner, not just Windows.

## 3. `TweakEngine`

Stateless (holds only the provider). Four operations:

- `Apply(tweak)` → writes `EnabledValue`.
- `Revert(tweak)` → writes `DisabledValue`.
- `Reset(tweak)` → writes `DefaultValue`, or deletes the value if `DefaultValue` is `null`.
- `GetState(tweak)` → reads the value, compares to enabled/disabled, returns `TweakState`.

Value comparison uses a private `ValuesEqual` helper (renamed from `Equals` to avoid
hiding `object.Equals`). It handles `null` and uses `Equals` semantics.

The engine intentionally contains **no UI, no async, no WinUI** — keeping it pure makes
it trivially unit-testable and keeps the GUI thin.

## 4. The WinUI 3 shell (`WinTuner.App`)

Three-column layout:

1. **Category rail** (`ListView` of `TweakCategory` names).
2. **Tweak list** (`ListView` of `RegistryTweak`, filtered by selected category).
3. **Detail pane** (`ScrollViewer`): title, thorough description, reference, live state,
   and Apply / Revert / Reset buttons + a status line.

Wiring:

- `MainWindow` constructs one `TweakEngine` over `RealRegistryProvider`.
- Selection of a category filters `Catalog.All`.
- Selection of a tweak populates the detail pane and refreshes its state.
- Buttons call `engine.Apply/Revert/Reset` then refresh the state read-out.

The app is **unpackaged** (`EnableMsixTooling=false`, `WindowsAppSDKSelfContained=true`)
so it builds and runs without MSIX packaging — the simplest deployment form and the one
that builds cleanly on CI.

## 5. Why headless tests but a real GUI build on CI

The GUI requires an interactive desktop session to *launch*, which CI runners do not
provide. However, the GUI **does** compile and link, and the WinAppSDK `.pri` resource
index **does** build, on `windows-latest` (which ships Visual Studio). So CI performs two
distinct, valuable checks:

1. **Build the real app** (`dotnet build`) → proves the XAML, code-behind, and packaging
   are correct and the WinAppSDK integrates.
2. **Run the engine tests** (`dotnet test`) → proves the tweak logic is correct against
   an in-memory registry.

This split is why logic lives in `WinTuner.Core` rather than in the app project.
