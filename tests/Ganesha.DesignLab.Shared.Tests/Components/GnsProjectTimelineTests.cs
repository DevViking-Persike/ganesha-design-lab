using Bunit;
using Ganesha.DesignLab.Shared.Components.Composites.ProjectTimeline;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsProjectTimelineTests : TestContext
{
    [Fact]
    public void RendersSummaryCategoriesAndTodayMarker()
    {
        var cut = RenderComponent<GnsProjectTimeline>(parameters => parameters
            .Add(p => p.Title, "Delivery plan")
            .Add(p => p.Tasks, BuildTasks())
            .Add(p => p.Categories, BuildCategories())
            .Add(p => p.Today, new DateOnly(2026, 4, 4)));

        Assert.Contains("Delivery plan", cut.Markup);
        Assert.Contains("Planning", cut.Markup);
        Assert.Contains("Design", cut.Markup);
        Assert.Contains("Total tasks", cut.Markup);
        Assert.NotNull(cut.Find(".gns-project-timeline__today-marker"));
        Assert.Equal(3, cut.FindAll(".gns-project-timeline__bar").Count);
    }

    [Fact]
    public void ToggleCategory_CollapsesAndExpandsTaskLabels()
    {
        var cut = RenderComponent<GnsProjectTimeline>(parameters => parameters
            .Add(p => p.Tasks, BuildTasks())
            .Add(p => p.Categories, BuildCategories())
            .Add(p => p.Today, new DateOnly(2026, 4, 4)));

        var planningButton = cut.FindAll(".gns-project-timeline__category-button")[0];

        Assert.Contains("Scope definition", cut.Markup);

        planningButton.Click();

        Assert.DoesNotContain("Scope definition", cut.Markup);

        planningButton = cut.FindAll(".gns-project-timeline__category-button")[0];
        planningButton.Click();

        Assert.Contains("Scope definition", cut.Markup);
    }

    [Fact]
    public void RendersEmptyState_WhenNoTasks()
    {
        var cut = RenderComponent<GnsProjectTimeline>(parameters => parameters
            .Add(p => p.Tasks, Array.Empty<ProjectTimelineTask>()));

        Assert.Contains("No timeline items", cut.Markup);
    }

    private static IReadOnlyList<ProjectTimelineCategory> BuildCategories()
    {
        return
        [
            new("Planning", "var(--gns-primary-500)"),
            new("Design", "var(--gns-violet-500)")
        ];
    }

    private static IReadOnlyList<ProjectTimelineTask> BuildTasks()
    {
        return
        [
            new(1, "Scope definition", new DateOnly(2026, 4, 1), new DateOnly(2026, 4, 3), 100, "Planning"),
            new(2, "Requirements mapping", new DateOnly(2026, 4, 2), new DateOnly(2026, 4, 5), 84, "Planning"),
            new(3, "Interface design", new DateOnly(2026, 4, 3), new DateOnly(2026, 4, 8), 55, "Design")
        ];
    }
}
