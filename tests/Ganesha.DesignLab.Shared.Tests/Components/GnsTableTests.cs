using Bunit;
using Ganesha.DesignLab.Shared.Components.DesignSystem.DataDisplay;
using Microsoft.AspNetCore.Components;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsTableTests : TestContext
{
    [Fact]
    public void RendersDefaultEmptyState_WhenNoItemsAndNotLoading()
    {
        var cut = RenderComponent<GnsTable<string>>(parameters => parameters
            .Add(p => p.Items, Array.Empty<string>())
            .Add(p => p.RowTemplate, item => builder =>
            {
                builder.OpenElement(0, "tr");
                builder.OpenElement(1, "td");
                builder.AddContent(2, item);
                builder.CloseElement();
                builder.CloseElement();
            }));

        Assert.Contains("No data available.", cut.Markup);
    }

    [Fact]
    public void LoadingState_RendersOverlayAndBusyAttribute()
    {
        var cut = RenderComponent<GnsTable<string>>(parameters => parameters
            .Add(p => p.Items, Array.Empty<string>())
            .Add(p => p.IsLoading, true)
            .Add(p => p.RowTemplate, item => builder =>
            {
                builder.OpenElement(0, "tr");
                builder.OpenElement(1, "td");
                builder.AddContent(2, item);
                builder.CloseElement();
                builder.CloseElement();
            }));

        Assert.NotNull(cut.Find(".gns-table__loading-overlay"));
        Assert.Equal("true", cut.Find("table").GetAttribute("aria-busy"));
    }

    [Fact]
    public void RendersItemsUsingRowTemplate()
    {
        var items = new[] { "A", "B" };

        var cut = RenderComponent<GnsTable<string>>(parameters => parameters
            .Add(p => p.Items, items)
            .Add(p => p.IsStriped, true)
            .Add(p => p.RowTemplate, item => builder =>
            {
                builder.OpenElement(0, "tr");
                builder.OpenElement(1, "td");
                builder.AddContent(2, item);
                builder.CloseElement();
                builder.CloseElement();
            }));

        var rows = cut.FindAll("tbody tr");

        Assert.Equal(2, rows.Count);
        Assert.Contains("A", rows[0].TextContent);
        Assert.Contains("B", rows[1].TextContent);
        Assert.Contains("gns-table--striped", cut.Find("table").ClassList);
    }
}
