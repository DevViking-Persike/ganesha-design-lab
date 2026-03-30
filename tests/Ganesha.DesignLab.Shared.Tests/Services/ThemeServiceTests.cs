using Ganesha.DesignLab.Shared.Services;

namespace Ganesha.DesignLab.Shared.Tests.Services;

public sealed class ThemeServiceTests
{
    [Fact]
    public void StartsWithLightTheme()
    {
        var service = new ThemeService();

        Assert.Equal("light", service.CurrentTheme);
        Assert.Contains("light", service.AvailableThemes);
        Assert.Contains("dark", service.AvailableThemes);
    }

    [Fact]
    public void SetTheme_ValidTheme_ChangesThemeAndRaisesEvent()
    {
        var service = new ThemeService();
        var raised = 0;
        service.OnThemeChanged += () => raised++;

        service.SetTheme("dark");

        Assert.Equal("dark", service.CurrentTheme);
        Assert.Equal(1, raised);
    }

    [Fact]
    public void SetTheme_InvalidTheme_DoesNotChangeThemeOrRaiseEvent()
    {
        var service = new ThemeService();
        var raised = 0;
        service.OnThemeChanged += () => raised++;

        service.SetTheme("sepia");

        Assert.Equal("light", service.CurrentTheme);
        Assert.Equal(0, raised);
    }

    [Fact]
    public void ToggleTheme_SwitchesBetweenLightAndDark()
    {
        var service = new ThemeService();

        service.ToggleTheme();
        Assert.Equal("dark", service.CurrentTheme);

        service.ToggleTheme();
        Assert.Equal("light", service.CurrentTheme);
    }
}
