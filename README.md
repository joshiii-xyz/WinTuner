# WinTuner — Windows Tweak Studio

> A Windows desktop application built with **WinUI 3** and the **Windows App SDK**
> (strictly Microsoft's stack: C# + .NET 8 + Windows App SDK + XAML). Its mission is
> to be a single, well-organized home for every safe, reversible system tweak we can
> faithfully apply to Windows — each one backed by a real, citable registry change.

This is a **from-scratch** project (not a port of any other tool). It is built to be
honest: every tweak ships with a thorough description of *exactly* what it changes, a
reference to the registry location, and an Apply / Revert / Reset-to-default action.
No "coming soon" placeholders, no speculative or biased claims — only what the setting
actually does.

---

## Table of contents

- [Design principles](#design-principles)
- [Architecture](#architecture)
- [Repository layout](#repository-layout)
- [Tweak taxonomy](#tweak-taxonomy)
- [Building locally](#building-locally)
- [Continuous integration](#continuous-integration)
- [Adding a new tweak](#adding-a-new-tweak)
- [Roadmap (honest status)](#roadmap-honest-status)

---

## Design principles

1. **Microsoft stack only.** WinUI 3 + Windows App SDK + C#/.NET. No PowerShell, no
   external scripts, no third-party binaries shipped or downloaded by the app.
2. **Everything is data.** A tweak is a declarative record (`RegistryTweak`). Adding a
   tweak means adding a record to the catalog — no new code path, no GUI wiring.
3. **Facts, not marketing.** Every description states the concrete registry change and
   its effect. We do not invent performance claims or "this makes Windows 10x faster".
4. **Reversible by design.** Every tweak can be reverted to a safe state and reset to
   the OS default. HKLM tweaks (which need elevation) are clearly marked.
5. **Testable core.** All tweak logic lives in a WinUI-free class library
   (`WinTuner.Core`) with an injectable registry abstraction, so it is unit-tested
   headless on every commit.
6. **No placeholders.** We do not list tweaks we have not implemented. The catalog only
   contains tweaks that are coded, described, and tested.

---

## Architecture

```
┌─────────────────────────────┐   uses    ┌──────────────────────────────┐
│   WinTuner.App (WinUI 3)    │ ────────▶ │      WinTuner.Core           │
│   - MainWindow.xaml(.cs)    │           │  - TweakEngine (logic)       │
│   - Declarative XAML UI     │           │  - Catalog (data records)    │
│   - No tweak logic here     │           │  - RegistryTweak (record)    │
└─────────────────────────────┘           │  - IRegistryProvider (abst.) │
                                           └───────────┬──────────────────┘
                                          implements    │      implements
                                           ┌────────────┴────────────┐
                                    ┌──────▼───────┐        ┌─────────▼─────────┐
                                    │ RealRegistry │        │  FakeRegistry    │
                                    │  (machine)   │        │ (unit tests only)│
                                    └──────────────┘        └──────────────────┘
```

- **`WinTuner.Core`** — platform-agnostic engine. Knows nothing about WinUI. This is
  what we unit-test.
- **`WinTuner.Core.Tests`** — xUnit tests asserting engine behavior against an
  in-memory `FakeRegistryProvider`, plus catalog-integrity rules (unique IDs,
  thorough descriptions, elevation flags correct).
- **`WinTuner.App`** — the WinUI 3 unpackaged desktop shell. Reads the catalog and
  drives the engine through `RealRegistryProvider`.

### Why this split?

The GUI cannot launch in CI (no interactive desktop session), but the *logic* can.
By keeping all tweak logic in `WinTuner.Core` behind `IRegistryProvider`, we get a
high-fidelity test gate that runs on every push — including on `windows-latest` — while
the GUI is still genuinely built and packaged by CI to prove it compiles and links.

---

## Repository layout

```
WinTuner/
├─ .github/workflows/ci.yml   # Build (x64 Release) + run tests on windows-latest
├─ docs/
│  ├─ ARCHITECTURE.md         # Deep-dive on the engine and data model
│  ├─ CONTRIBUTING.md         # How to add a tweak + catalog rules
│  └─ CATEGORIES.md           # The full tweak taxonomy and what each covers
├─ src/
│  ├─ WinTuner.Core/          # Engine + catalog + registry abstraction
│  ├─ WinTuner.Core.Tests/    # xUnit suite
│  └─ WinTuner.App/           # WinUI 3 unpackaged desktop app
├─ Directory.Build.props      # Shared build properties
├─ WinTuner.sln               # Solution (hand-authored; see note below)
└─ README.md
```

> **Note on the `.sln`:** `dotnet new sln` is unavailable in the SDK install used
> during initial authoring, so the solution file is hand-authored with stable
> project GUIDs. It opens fine in Visual Studio and builds via `dotnet build`.

---

## Tweak taxonomy

Tweaks are grouped into categories shown in the navigation rail. See
[`docs/CATEGORIES.md`](docs/CATEGORIES.md) for the full taxonomy and what each
category covers. Current categories:

| Category     | Scope                                                        |
|--------------|-------------------------------------------------------------|
| `Explorer`   | File Explorer behavior (extensions, hidden files, drives)   |
| `Privacy`    | Advertising ID, tailored experiences, telemetry consent     |
| `Performance`| Visual-effects / scheduling toggles (planned, not yet coded)|
| `Appearance` | Theming, taskbar, accent color (planned, not yet coded)     |
| `System`     | OS-level policy toggles (e.g. Copilot)                      |
| `Security`   | Defender / SmartScreen / UAC posture (planned)              |
| `Network`   | TCP/DNS/adapter tweaks (planned)                            |
| `Gaming`    | Game Mode / GPU scheduling (planned)                        |

Only categories with coded, tested tweaks appear populated. Empty categories are
**not** faked into the UI.

---

## Building locally

**Prerequisites**

- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (17.x) with the **Windows App SDK** / **.NET Desktop** workload
  — required to compile the WinUI 3 `.pri` resource index locally. The `.Core`
  library and its tests build with the .NET SDK alone.

**Commands**

```pwsh
# Restore + build everything (x64, Release)
dotnet build WinTuner.sln -c Release -p:Platform=x64

# Run the headless engine tests (no GUI needed)
dotnet test src/WinTuner.Core.Tests/WinTuner.Core.Tests.csproj -c Release -p:Platform=x64

# Launch the app (requires the WinUI 3 runtime; unpackaged build output under
# src/WinTuner.App/bin/x64/Release/net8.0-windows10.0.26100.0/WinTuner.exe)
```

> **Tip:** HKLM tweaks (e.g. "Disable Windows Copilot") require the app to be run
> **as administrator**. The UI marks such tweaks with "(requires administrator)".

---

## Continuous integration

Every push and pull request triggers [`.github/workflows/ci.yml`](.github/workflows/ci.yml)
on **`windows-latest`** (GitHub-hosted runner with full Visual Studio + Windows SDK):

1. Restore all projects.
2. `dotnet build WinTuner.sln -c Release -p:Platform=x64` — this **compiles and
   links the real WinUI 3 app**, proving the GUI is buildable.
3. `dotnet test` — runs the `WinTuner.Core.Tests` suite headless.
4. On success, uploads the built app binaries as a downloadable artifact.

This gives unlimited, free CI builds for a public repo and a real correctness gate on
the tweak engine.

---

## Adding a new tweak

Adding a tweak is **data-only**. See [`docs/CONTRIBUTING.md`](docs/CONTRIBUTING.md)
for the full rules. In short, append a `RegistryTweak` record to
`src/WinTuner.Core/Tweaks/Catalog.cs`:

```csharp
new()
{
    Id = "explorer.show-file-extensions",
    Title = "Show file name extensions",
    Description = "…thorough, factual description of the registry change…",
    Category = TweakCategory.Explorer,
    Hive = RegistryHive.CurrentUser,
    SubKey = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
    ValueName = "HideFileExt",
    ValueKind = RegistryValueKind.DWord,
    EnabledValue = 0,
    DisabledValue = 1,
    DefaultValue = 1,                 // null => delete the value on Reset
    AbsentState = TweakState.Disabled, // what "value absent" means for this tweak
    Reference = "HKCU\\…\\Advanced!HideFileExt (0 = show, 1 = hide)",
}
```

The UI, the engine, and the tests all pick it up automatically. No GUI code to write.

---

## Roadmap (honest status)

We build in the open and never claim a tweak exists before it is coded + tested.

- ✅ Engine + registry abstraction + in-memory fake
- ✅ xUnit suite (8 tests) covering apply/revert/reset/state + catalog integrity
- ✅ WinUI 3 unpackaged shell (category rail, tweak list, detail + apply/revert/reset)
- ✅ CI: builds the real app + runs tests on `windows-latest`
- ✅ Seeded catalog: File Explorer (3), Privacy (2), System/Copilot (1)
- 🔜 Expand catalogs across all categories (Appearance, Performance, Security,
       Network, Gaming) — each as data records with citable references
- 🔜 One-click "apply all in category" + export/import of a tweak profile
- 🔜 Elevation-aware flow (auto-relaunch as admin when an HKLM tweak is applied)

Legend: ✅ done · 🔜 not yet implemented (no fake entries in the UI)
