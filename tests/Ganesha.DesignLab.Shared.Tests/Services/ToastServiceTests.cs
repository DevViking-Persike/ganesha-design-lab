using Ganesha.DesignLab.Shared.Models.Feedback;
using Ganesha.DesignLab.Shared.Services;

namespace Ganesha.DesignLab.Shared.Tests.Services;

public sealed class ToastServiceTests
{
    [Fact]
    public void ShowSuccess_AddsToastAndRaisesEvent()
    {
        using var service = new ToastService();
        ToastMessage? raisedToast = null;
        service.OnToastAdded += toast => raisedToast = toast;

        service.ShowSuccess("Saved", "Success", durationMs: 0);

        var activeToasts = service.GetActiveToasts();
        var toast = Assert.Single(activeToasts);

        Assert.Equal("Saved", toast.Message);
        Assert.Equal("Success", toast.Title);
        Assert.Equal(ToastSeverity.Success, toast.Severity);
        Assert.NotNull(raisedToast);
        Assert.Equal(toast.Id, raisedToast!.Id);
    }

    [Fact]
    public void DismissToast_RemovesToast()
    {
        using var service = new ToastService();
        service.ShowInfo("Heads up", durationMs: 0);

        var toast = Assert.Single(service.GetActiveToasts());

        service.DismissToast(toast.Id);

        Assert.Empty(service.GetActiveToasts());
    }
}
