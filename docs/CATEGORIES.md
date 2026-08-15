# Tweak taxonomy

Every tweak belongs to exactly one `TweakCategory`. The category drives the navigation
rail in the UI and groups related settings. This document defines each category's
scope and the *kind* of tweaks it should contain, so the catalog grows in an organized
way rather than as a flat list.

> **Status convention:** categories below are marked ✅ populated (has coded, tested
> tweaks) or 🔜 planned (no tweaks yet). The UI only shows categories that contain
> tweaks; we never show an empty "coming soon" category.

---

## ✅ Explorer
File Explorer behavior and visibility settings.

- `explorer.show-file-extensions` — Show file name extensions (`HideFileExt`).
- `explorer.show-hidden-files` — Show hidden files/folders/drives (`Hidden`).
- `explorer.hide-drive-letters` — Hide letters for empty drives (`HideDrivesWithNoMedia`).

*Planned within this category:* remove OneDrive from Explorer namespace, disable
Quick Access / show This PC by default, disable search history, toggle compact view.

## ✅ Privacy
Settings that reduce OS-level data collection and personalization.

- `privacy.disable-advertising-id` — Advertising ID (`AdvertisingInfo!Enabled`).
- `privacy.disable-tailored-experiences` — Tailored experiences with diagnostic data
  (`Privacy!TailoredExperiencesWithDiagnosticDataEnabled`).

*Planned:* disable activity history / Timeline, disable clipboard cloud sync,
disable location history, turn off suggested content in Settings.

## ✅ System
OS-level policy toggles applied via the administrative policy keys.

- `system.disable-windows-copilot` — Disable Windows Copilot
  (`HKLM\…\WindowsCopilot!TurnOffWindowsCopilot`). **Requires elevation.**

*Planned:* disable Windows Tips, disable Consumer Features (Spotlight/Meet Now),
disable background app access globally.

## 🔜 Performance
Visual-effects and scheduling toggles that trade eye-candy for throughput.

*Planned:* disable transparency effects, disable animations, set "Adjust for best
performance", disable startup delay, disable search indexing on selected paths,
disable Game DVR background recording, disable SysMain (Superfetch).

## 🔜 Appearance
Theming and shell personalization.

*Planned:* accent color on title bars, dark mode toggle, hide taskbar search/Widgets/
Task View buttons, show seconds in the clock, disable centered taskbar, small taskbar.

## 🔜 Security
Defender / SmartScreen / UAC posture.

*Planned:* UAC level, disable SmartScreen for apps/files (with warning), disable
defender cloud-delivered protection (with warning), enable ransomware-controlled-folder
access, disable removable-drive autoplay.

> Security tweaks are sensitive: each ships with an explicit warning about the trade-off
> and is clearly marked. We will **never** ship a tweak that silently weakens the
> machine's security without an obvious, factual warning in its description.

## 🔜 Network
TCP/IP, DNS, and adapter behavior.

*Planned:* disable IPv6 (with caveat), set custom DNS (Cloudflare/Quad9), disable Nagle's
algorithm, enable TCP Fast Open, disable NETBIOS over TCP/IP, toggle metered-connection
behavior.

## 🔜 Gaming
Game Mode and GPU scheduling.

*Planned:* enable Game Mode, enable Hardware-Accelerated GPU Scheduling, disable Game DVR,
set GPU preference per-app, disable fullscreen optimizations globally.

---

## Adding a category

If a tweak does not fit an existing category, extend `TweakCategory` in
`src/WinTuner.Core/Tweaks/TweakCategory.cs`, document it here, and only surface it once
it contains at least one coded, tested tweak.
