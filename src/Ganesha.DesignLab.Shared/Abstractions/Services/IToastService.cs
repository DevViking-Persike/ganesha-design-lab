using Ganesha.DesignLab.Shared.Models.Feedback;

namespace Ganesha.DesignLab.Shared.Abstractions.Services;

public interface IToastService
{
    event Action<ToastMessage>? OnToastAdded;
    void ShowSuccess(string message, string? title = null, int? durationMs = null);
    void ShowError(string message, string? title = null, int? durationMs = null);
    void ShowWarning(string message, string? title = null, int? durationMs = null);
    void ShowInfo(string message, string? title = null, int? durationMs = null);
    IReadOnlyList<ToastMessage> GetActiveToasts();
    void DismissToast(Guid id);
}
