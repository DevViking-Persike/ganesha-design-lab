using Ganesha.DesignLab.Shared.Abstractions.Navigation;

namespace Ganesha.DesignLab.Shared.Models.Navigation;

public class NavigationItemModel : INavigationItem
{
    public string Label { get; set; } = string.Empty;
    public string? Icon { get; set; }
    public string? Href { get; set; }
    public bool IsActive { get; set; }
    public IReadOnlyList<INavigationItem>? Children { get; set; }
    public string? Badge { get; set; }
    public bool IsExpanded { get; set; }
}
