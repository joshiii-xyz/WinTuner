using System.Linq;
using Microsoft.Win32;
using WinTuner.Core.Registry;
using WinTuner.Core.Profile;
using WinTuner.Core.Tweaks;
using Xunit;

namespace WinTuner.Core.Tests;

public class TweakEngineTests
{
    private const string TestSubKey = @"Software\WinTunerTest";

    private static RegistryTweak Sample() => new()
    {
        Id = "test.sample",
        Title = "Sample",
        Description = "Sample tweak used by the unit tests.",
        Category = TweakCategory.System,
        Hive = RegistryHive.CurrentUser,
        SubKey = TestSubKey,
        ValueName = "Flag",
        ValueKind = RegistryValueKind.DWord,
        EnabledValue = 1,
        DisabledValue = 0,
        DefaultValue = 0,
        AbsentState = TweakState.Disabled,
    };

    [Fact]
    public void Apply_WritesEnabledValue_AndReportsEnabled()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = Sample();

        engine.Apply(tweak);

        Assert.Equal(1, fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "Flag"));
        Assert.Equal(TweakState.Enabled, engine.GetState(tweak));
    }

    [Fact]
    public void Revert_WritesDisabledValue_AndReportsDisabled()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = Sample();

        engine.Apply(tweak);
        engine.Revert(tweak);

        Assert.Equal(0, fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "Flag"));
        Assert.Equal(TweakState.Disabled, engine.GetState(tweak));
    }

    [Fact]
    public void Reset_DeletesValue_WhenDefaultIsNull()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = Sample() with { DefaultValue = null };

        engine.Apply(tweak);
        Assert.NotNull(fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "Flag"));

        engine.Reset(tweak);

        Assert.Null(fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "Flag"));
    }

    [Fact]
    public void Reset_WritesDefaultValue_WhenDefaultIsSet()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = Sample();

        engine.Apply(tweak);
        engine.Reset(tweak);

        Assert.Equal(0, fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "Flag"));
    }

    [Fact]
    public void GetState_Absent_ReturnsDeclaredAbsentState()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = Sample();

        Assert.Equal(TweakState.Disabled, engine.GetState(tweak));
    }

    [Fact]
    public void Catalog_AllTweaks_HaveUniqueIds()
    {
        var ids = Catalog.All.Select(t => t.Id).ToList();
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    [Fact]
    public void Catalog_AllTweaks_HaveThoroughDescriptions()
    {
        foreach (var t in Catalog.All)
        {
            Assert.False(string.IsNullOrWhiteSpace(t.Description), $"Tweak {t.Id} is missing a description.");
            Assert.True(t.Description.Length >= 60, $"Tweak {t.Id} description is too short to be thorough.");
            Assert.False(string.IsNullOrWhiteSpace(t.Reference), $"Tweak {t.Id} is missing a reference.");
        }
    }

    [Fact]
    public void Catalog_HklmTweaks_RequireElevation()
    {
        foreach (var t in Catalog.All.Where(t => t.Hive == RegistryHive.LocalMachine))
        {
            Assert.True(t.RequiresElevation, $"Tweak {t.Id} writes to HKLM but does not report RequiresElevation.");
        }
    }

    [Fact]
    public void Catalog_AllTweaks_HaveValidHiveAndKind()
    {
        foreach (var t in Catalog.All)
        {
            Assert.True(Enum.IsDefined(typeof(RegistryHive), t.Hive), $"Tweak {t.Id} has an invalid hive.");
            Assert.True(Enum.IsDefined(typeof(RegistryValueKind), t.ValueKind), $"Tweak {t.Id} has an invalid value kind.");
        }
    }

    [Fact]
    public void Apply_StringValuedTweak_WritesString()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var tweak = new RegistryTweak
        {
            Id = "test.string",
            Title = "String sample",
            Description = "A string-valued tweak used to exercise non-DWord kinds in the engine.",
            Category = TweakCategory.Performance,
            Hive = RegistryHive.CurrentUser,
            SubKey = TestSubKey,
            ValueName = "MenuShowDelay",
            ValueKind = RegistryValueKind.String,
            EnabledValue = "0",
            DisabledValue = "400",
            DefaultValue = "400",
            AbsentState = TweakState.Disabled,
        };

        engine.Apply(tweak);
        Assert.Equal("0", fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "MenuShowDelay"));

        engine.Revert(tweak);
        Assert.Equal("400", fake.GetValue(RegistryHive.CurrentUser, TestSubKey, "MenuShowDelay"));
    }

    [Fact]
    public void Catalog_ContainsStringValuedTweaks()
    {
        var stringTweaks = Catalog.All.Where(t => t.ValueKind == RegistryValueKind.String).ToList();
        Assert.NotEmpty(stringTweaks);
        foreach (var t in stringTweaks)
        {
            Assert.False(string.IsNullOrWhiteSpace((string?)t.EnabledValue), $"String tweak {t.Id} has no EnabledValue.");
            Assert.False(string.IsNullOrWhiteSpace((string?)t.DisabledValue), $"String tweak {t.Id} has no DisabledValue.");
        }
    }

    [Fact]
    public void Catalog_AllTweaks_HaveNonNullKeyAndValueName()
    {
        foreach (var t in Catalog.All)
        {
            Assert.False(string.IsNullOrWhiteSpace(t.SubKey), $"Tweak {t.Id} is missing SubKey.");
            Assert.False(string.IsNullOrWhiteSpace(t.ValueName), $"Tweak {t.Id} is missing ValueName.");
        }
    }

    [Fact]
    public void Profile_ExportThenApply_RoundTripsState()
    {
        var fake = new FakeRegistryProvider();
        var engine = new TweakEngine(fake);
        var sample = Sample();
        var other = new RegistryTweak
        {
            Id = "test.other",
            Title = "Other",
            Description = "Second tweak used to verify profile import touches only recorded entries.",
            Category = TweakCategory.Privacy,
            Hive = RegistryHive.CurrentUser,
            SubKey = TestSubKey,
            ValueName = "OtherFlag",
            ValueKind = RegistryValueKind.DWord,
            EnabledValue = 1,
            DisabledValue = 0,
            DefaultValue = 0,
            AbsentState = TweakState.Disabled,
        };

        // Enable only the sample tweak, then export the live state.
        engine.Apply(sample);
        engine.Revert(other);
        var json = ProfileService.Export(new[] { sample, other }, engine);

        // Change both states on the fake store, then replay the profile.
        engine.Revert(sample);
        engine.Apply(other);
        var states = ProfileService.Parse(json);
        ProfileService.Apply(new[] { sample, other }, states, engine);

        Assert.Equal(TweakState.Enabled, engine.GetState(sample));
        Assert.Equal(TweakState.Disabled, engine.GetState(other));
    }

    [Fact]
    public void Profile_Parse_ReturnsEmpty_OnInvalidJson()
    {
        var states = ProfileService.Parse("this is not json");
        Assert.Empty(states);
    }
}
