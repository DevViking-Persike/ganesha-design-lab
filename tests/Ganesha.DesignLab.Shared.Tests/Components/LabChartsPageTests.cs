using Bunit;
using Ganesha.DesignLab.Shared.Components.Lab.Pages;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class LabChartsPageTests : TestContext
{
    [Fact]
    public void RendersCoreSections_AndInfographicCatalog()
    {
        var cut = RenderComponent<LabCharts>();

        Assert.Contains("Charts", cut.Markup);
        Assert.Contains("Bar Charts", cut.Markup);
        Assert.Contains("Line Charts", cut.Markup);
        Assert.Contains("Donut Charts", cut.Markup);
        Assert.Contains("Progress Indicators", cut.Markup);
        Assert.Contains("Infographic Charts", cut.Markup);
        Assert.Contains("Planning Timeline", cut.Markup);
    }

    [Fact]
    public void RendersKeyInfographicExamples()
    {
        var cut = RenderComponent<LabCharts>();

        Assert.Contains("Gauge Charts", cut.Markup);
        Assert.Contains("Quarterly Revenue by Region", cut.Markup);
        Assert.Contains("Funnel Variations", cut.Markup);
        Assert.Contains("Project Delivery Timeline", cut.Markup);
    }
}
