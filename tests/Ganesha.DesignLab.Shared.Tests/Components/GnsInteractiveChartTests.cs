using Bunit;
using Ganesha.DesignLab.Shared.Components.Composites.InteractiveChart;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsInteractiveChartTests : TestContext
{
    [Fact]
    public void RendersFirstOptionSelected_ByDefault()
    {
        var cut = RenderComponent<GnsInteractiveChart>(parameters => parameters
            .Add(p => p.Title, "Revenue")
            .Add(p => p.Options, BuildOptions())
            .Add(p => p.ChartType, InteractiveChartType.Bar));

        var selected = cut.Find(".gns-interactive-chart__option--selected");

        Assert.Contains("Week", selected.TextContent);
        Assert.Contains("Mon", cut.Markup);
        Assert.Contains("Tue", cut.Markup);
    }

    [Fact]
    public void ClickingOption_ChangesSelection_AndInvokesCallback()
    {
        string? selectedId = null;

        var cut = RenderComponent<GnsInteractiveChart>(parameters => parameters
            .Add(p => p.Title, "Revenue")
            .Add(p => p.Options, BuildOptions())
            .Add(p => p.ChartType, InteractiveChartType.Line)
            .Add(p => p.OnOptionSelected, EventCallback.Factory.Create<string>(this, id => selectedId = id)));

        cut.FindAll(".gns-interactive-chart__option")[1].Click();

        Assert.Equal("month", selectedId);
        Assert.Contains("gns-interactive-chart__option--selected", cut.FindAll(".gns-interactive-chart__option")[1].ClassName);
        Assert.Contains("Jan", cut.Markup);
        Assert.Contains("Feb", cut.Markup);
    }

    [Fact]
    public void BubbleType_RendersPlaceholderMessage()
    {
        var cut = RenderComponent<GnsInteractiveChart>(parameters => parameters
            .Add(p => p.Title, "Markets")
            .Add(p => p.Options, BuildOptions())
            .Add(p => p.ChartType, InteractiveChartType.Bubble));

        Assert.Contains("Bubble chart is best visualized directly via GnsBubbleChart", cut.Markup);
    }

    private static InteractiveChartOption[] BuildOptions()
    {
        return
        [
            new("week", "Week",
            [
                new("Mon", 10),
                new("Tue", 14)
            ]),
            new("month", "Month",
            [
                new("Jan", 120),
                new("Feb", 160)
            ])
        ];
    }
}
