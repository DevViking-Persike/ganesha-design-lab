using Bunit;
using Ganesha.DesignLab.Shared.Components.DesignSystem.Charts;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsChartComponentsTests : TestContext
{
    [Fact]
    public void Gauge_RendersValueLabelsAndFilledBand()
    {
        var cut = RenderComponent<GnsGaugeChart>(parameters => parameters
            .Add(p => p.Value, 99.9)
            .Add(p => p.MaxValue, 100d)
            .Add(p => p.ValueLabel, "99.9%")
            .Add(p => p.Label, "Uptime")
            .Add(p => p.MinLabel, "0")
            .Add(p => p.MaxLabel, "100")
            .Add(p => p.IsAnimated, false));

        Assert.Equal("img", cut.Find(".gns-gauge").GetAttribute("role"));
        Assert.Contains("99.9%", cut.Markup);
        Assert.Contains("Uptime", cut.Markup);
        Assert.Contains(">0<", cut.Markup);
        Assert.Contains(">100<", cut.Markup);
        Assert.NotNull(cut.Find(".gns-gauge__fill"));
        Assert.NotNull(cut.Find(".gns-gauge__gloss"));
    }

    [Fact]
    public void Gauge_GeneratesDistinctSvgIdsAcrossRenders()
    {
        var first = RenderComponent<GnsGaugeChart>(parameters => parameters
            .Add(p => p.Value, 65d)
            .Add(p => p.MaxValue, 100d));

        var second = RenderComponent<GnsGaugeChart>(parameters => parameters
            .Add(p => p.Value, 65d)
            .Add(p => p.MaxValue, 100d));

        var firstGradientId = first.Find("linearGradient[id^='gns-gauge-fill-']").Id;
        var secondGradientId = second.Find("linearGradient[id^='gns-gauge-fill-']").Id;

        Assert.NotEqual(firstGradientId, secondGradientId);
    }

    [Fact]
    public void Funnel_RendersReadableCompactValues()
    {
        var cut = RenderComponent<GnsFunnelChart>(parameters => parameters
            .Add(p => p.Data, new[]
            {
                new ChartDataPoint("Visitors", 18_400),
                new ChartDataPoint("Leads", 7_260),
                new ChartDataPoint("Qualified", 2_940)
            })
            .Add(p => p.IsAnimated, false));

        Assert.Contains("Visitors", cut.Markup);
        Assert.Contains("Leads", cut.Markup);
        Assert.Contains("18.4K", cut.Markup);
        Assert.DoesNotContain("E+04", cut.Markup, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(3, cut.FindAll(".gns-funnel__shape").Count);
    }

    [Fact]
    public void Funnel_RendersEmptyState_WhenNoData()
    {
        var cut = RenderComponent<GnsFunnelChart>(parameters => parameters
            .Add(p => p.Data, Array.Empty<ChartDataPoint>()));

        Assert.Contains("No data", cut.Markup);
    }

    [Fact]
    public void StackedBar_RendersGridLegendAndLabels()
    {
        var cut = RenderComponent<GnsStackedBarChart>(parameters => parameters
            .Add(p => p.Labels, new[] { "Q1", "Q2" })
            .Add(p => p.Series, new[]
            {
                new ChartSeries("North America", new double[] { 320, 420 }),
                new ChartSeries("Europe", new double[] { 180, 240 }),
                new ChartSeries("LATAM", new double[] { 90, 110 })
            })
            .Add(p => p.IsAnimated, false));

        Assert.Equal(6, cut.FindAll(".gns-stacked-bar__bar").Count);
        Assert.Contains("North America", cut.Markup);
        Assert.Contains("Europe", cut.Markup);
        Assert.Contains("LATAM", cut.Markup);
        Assert.Contains("Q1", cut.Markup);
        Assert.Contains("Q2", cut.Markup);
        Assert.NotEmpty(cut.FindAll(".gns-stacked-bar__grid-line"));
    }

    [Fact]
    public void StackedBar_RendersEmptyState_WhenSeriesOrLabelsMissing()
    {
        var cut = RenderComponent<GnsStackedBarChart>(parameters => parameters
            .Add(p => p.Labels, Array.Empty<string>())
            .Add(p => p.Series, Array.Empty<ChartSeries>()));

        Assert.Contains("No data", cut.Markup);
    }

    [Fact]
    public void Pie_RendersSlicesAndLegendPercentages()
    {
        var cut = RenderComponent<GnsPieChart>(parameters => parameters
            .Add(p => p.Data, new[]
            {
                new ChartDataPoint("Sales", 42),
                new ChartDataPoint("Marketing", 28),
                new ChartDataPoint("Ops", 30)
            })
            .Add(p => p.IsAnimated, false));

        Assert.Equal(3, cut.FindAll(".gns-pie-chart__slice").Count);
        Assert.Equal(3, cut.FindAll(".gns-pie-chart__legend-item").Count);
        Assert.Contains("Sales", cut.Markup);
        Assert.Contains("42.0%", cut.Markup);
    }

    [Fact]
    public void Pie_GeneratesDistinctGradientIdsAcrossRenders()
    {
        var data = new[]
        {
            new ChartDataPoint("A", 1),
            new ChartDataPoint("B", 2)
        };

        var first = RenderComponent<GnsPieChart>(parameters => parameters
            .Add(p => p.Data, data)
            .Add(p => p.IsAnimated, false));

        var second = RenderComponent<GnsPieChart>(parameters => parameters
            .Add(p => p.Data, data)
            .Add(p => p.IsAnimated, false));

        var firstGradientId = first.Find("linearGradient[id^='gns-pie-grad-']").Id;
        var secondGradientId = second.Find("linearGradient[id^='gns-pie-grad-']").Id;

        Assert.NotEqual(firstGradientId, secondGradientId);
    }

    [Fact]
    public void Pie_RendersEmptyState_WhenNoData()
    {
        var cut = RenderComponent<GnsPieChart>(parameters => parameters
            .Add(p => p.Data, Array.Empty<ChartDataPoint>()));

        Assert.Contains("No data", cut.Markup);
    }

    [Fact]
    public void Process_RendersAllStepsValuesAndDescriptions()
    {
        var cut = RenderComponent<GnsProcessChart>(parameters => parameters
            .Add(p => p.Steps, new[]
            {
                new ProcessStep("Discover", "24", "Research backlog"),
                new ProcessStep("Build", "16", "Core implementation"),
                new ProcessStep("Launch", "6", "Release checklist")
            })
            .Add(p => p.IsAnimated, false));

        Assert.Equal("region", cut.Find(".gns-process-chart").GetAttribute("role"));
        Assert.Equal(3, cut.FindAll(".gns-process-chart__step").Count);
        Assert.Contains("Discover", cut.Markup);
        Assert.Contains("Core implementation", cut.Markup);
        Assert.Contains("Launch", cut.Markup);
    }

    [Fact]
    public void Bubble_RendersBubblesLabelsAndValues()
    {
        var cut = RenderComponent<GnsBubbleChart>(parameters => parameters
            .Add(p => p.Data, new[]
            {
                new BubbleDataPoint("North", 12, 84, 32),
                new BubbleDataPoint("Europe", 22, 72, 24),
                new BubbleDataPoint("LATAM", 9, 58, 18)
            })
            .Add(p => p.IsAnimated, false));

        Assert.Equal(3, cut.FindAll(".gns-bubble-chart__bubble").Count);
        Assert.Equal(3, cut.FindAll(".gns-bubble-chart__label").Count);
        Assert.Contains("North", cut.Markup);
        Assert.Contains(">32<", cut.Markup);
        Assert.NotEmpty(cut.FindAll(".gns-bubble-chart__grid-h"));
        Assert.NotEmpty(cut.FindAll(".gns-bubble-chart__grid-v"));
    }

    [Fact]
    public void Bubble_GeneratesDistinctFilterIdsAcrossRenders()
    {
        var data = new[]
        {
            new BubbleDataPoint("A", 1, 2, 10)
        };

        var first = RenderComponent<GnsBubbleChart>(parameters => parameters
            .Add(p => p.Data, data)
            .Add(p => p.IsAnimated, false));

        var second = RenderComponent<GnsBubbleChart>(parameters => parameters
            .Add(p => p.Data, data)
            .Add(p => p.IsAnimated, false));

        var firstFilterId = first.Find("filter[id^='gns-bubble-shadow-']").Id;
        var secondFilterId = second.Find("filter[id^='gns-bubble-shadow-']").Id;

        Assert.NotEqual(firstFilterId, secondFilterId);
    }

    [Fact]
    public void Bubble_RendersEmptyState_WhenNoData()
    {
        var cut = RenderComponent<GnsBubbleChart>(parameters => parameters
            .Add(p => p.Data, Array.Empty<BubbleDataPoint>()));

        Assert.Contains("No data", cut.Markup);
    }
}
