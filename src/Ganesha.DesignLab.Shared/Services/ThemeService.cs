using Ganesha.DesignLab.Shared.Abstractions.Services;

namespace Ganesha.DesignLab.Shared.Services;

public sealed class ThemeService : IThemeService
{
    private string _currentTheme = "light";

    private static readonly List<string> Themes = ["light", "dark"];

    public string CurrentTheme => _currentTheme;

    public IReadOnlyList<string> AvailableThemes => Themes.AsReadOnly();

    public event Action? OnThemeChanged;

    public void SetTheme(string theme)
    {
        if (!Themes.Contains(theme) || _currentTheme == theme)
            return;

        _currentTheme = theme;
        OnThemeChanged?.Invoke();
    }

    public void ToggleTheme()
    {
        var next = _currentTheme == "light" ? "dark" : "light";
        SetTheme(next);
    }
}
