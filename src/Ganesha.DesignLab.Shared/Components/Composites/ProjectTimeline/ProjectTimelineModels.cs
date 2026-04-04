namespace Ganesha.DesignLab.Shared.Components.Composites.ProjectTimeline;

public record ProjectTimelineTask(
    int Id,
    string Name,
    DateOnly StartDate,
    DateOnly EndDate,
    int Progress,
    string Category,
    string? AccentColor = null);

public record ProjectTimelineCategory(
    string Name,
    string? AccentColor = null,
    string? ShortLabel = null);
