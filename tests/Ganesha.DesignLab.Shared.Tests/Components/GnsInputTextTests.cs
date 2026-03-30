using Bunit;
using Ganesha.DesignLab.Shared.Components.DesignSystem.Form;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsInputTextTests : TestContext
{
    [Fact]
    public void RendersLabelHelperAndAccessibilityAttributes()
    {
        var cut = RenderComponent<GnsInputText>(parameters => parameters
            .Add(p => p.Label, "Email")
            .Add(p => p.HelperText, "Use your work email")
            .Add(p => p.IsRequired, true)
            .Add(p => p.Value, "dev@example.com"));

        var input = cut.Find("input");
        var label = cut.Find("label");
        var helper = cut.Find(".gns-input-text__helper");

        Assert.Equal("Email*", label.TextContent.Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty));
        Assert.Equal("true", input.GetAttribute("aria-required"));
        Assert.NotNull(input.GetAttribute("aria-describedby"));
        Assert.Equal("Use your work email", helper.TextContent.Trim());
    }

    [Fact]
    public void Input_RaisesValueChanged_WhenEditable()
    {
        string? changed = null;

        var cut = RenderComponent<GnsInputText>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string>(this, value => changed = value)));

        cut.Find("input").Input("new value");

        Assert.Equal("new value", changed);
    }

    [Fact]
    public void Input_DoesNotRaiseValueChanged_WhenReadOnly()
    {
        string? changed = null;

        var cut = RenderComponent<GnsInputText>(parameters => parameters
            .Add(p => p.Value, string.Empty)
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.ValueChanged, EventCallback.Factory.Create<string>(this, value => changed = value)));

        cut.Find("input").Input("new value");

        Assert.Null(changed);
    }
}
