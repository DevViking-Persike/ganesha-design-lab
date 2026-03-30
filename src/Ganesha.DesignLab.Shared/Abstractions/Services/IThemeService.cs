namespace Ganesha.DesignLab.Shared.Abstractions.Services;

public interface IThemeService
{
    string CurrentTheme { get; }
    IReadOnlyList<string> AvailableThemes { get; }
    event Action? OnThemeChanged;
    void SetTheme(string theme);
    void ToggleTheme();
}
