using System.Linq;
using Microsoft.Win32;
using WinTuner.Core.Registry;
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
}
