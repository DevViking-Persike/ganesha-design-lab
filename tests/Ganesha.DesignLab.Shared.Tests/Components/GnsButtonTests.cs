using Bunit;
using Ganesha.DesignLab.Shared.Components.DesignSystem.Actions;

namespace Ganesha.DesignLab.Shared.Tests.Components;

public sealed class GnsButtonTests : TestContext
{
    [Fact]
    public void RendersLabelAndVariantAndSizeClasses()
    {
        var cut = RenderComponent<GnsButton>(parameters => parameters
            .Add(p => p.Label, "Save")
            .Add(p => p.Variant, ButtonVariant.Outline)
            .Add(p => p.Size, ButtonSize.Large));

        var button = cut.Find("button");

        Assert.Contains("Save", button.TextContent);
        Assert.Contains("gns-button--outline", button.ClassList);
        Assert.Contains("gns-button--lg", button.ClassList);
    }

    [Fact]
    public void LoadingState_SetsBusyAndSuppressesClick()
    {
        var clicks = 0;

        var cut = RenderComponent<GnsButton>(parameters => parameters
            .Add(p => p.Label, "Save")
            .Add(p => p.IsLoading, true)
            .Add(p => p.OnClick, EventCallback.Factory.Create(this, () => clicks++)));

        var button = cut.Find("button");
        button.Click();

        Assert.Equal("true", button.GetAttribute("aria-busy"));
        Assert.Equal("true", button.GetAttribute("aria-disabled"));
        Assert.Equal(0, clicks);
    }
}
