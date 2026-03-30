namespace Ganesha.DesignLab.Shared.Abstractions.Navigation;

public interface INavigationItem
{
    string Label { get; }
    string? Icon { get; }
    string? Href { get; }
    bool IsActive { get; }
    IReadOnlyList<INavigationItem>? Children { get; }
}
