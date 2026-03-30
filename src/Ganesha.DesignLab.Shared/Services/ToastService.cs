using Ganesha.DesignLab.Shared.Abstractions.Services;
using Ganesha.DesignLab.Shared.Models.Feedback;

namespace Ganesha.DesignLab.Shared.Services;

public sealed class ToastService : IToastService, IDisposable
{
    private const int DefaultDurationMs = 4000;

    private readonly List<ToastMessage> _toasts = [];
    private readonly Lock _lock = new();

    public event Action<ToastMessage>? OnToastAdded;

    public void ShowSuccess(string message, string? title = null, int? durationMs = null)
        => Add(message, title, ToastSeverity.Success, durationMs ?? DefaultDurationMs);

    public void ShowError(string message, string? title = null, int? durationMs = null)
        => Add(message, title, ToastSeverity.Error, durationMs ?? DefaultDurationMs);

    public void ShowWarning(string message, string? title = null, int? durationMs = null)
        => Add(message, title, ToastSeverity.Warning, durationMs ?? DefaultDurationMs);

    public void ShowInfo(string message, string? title = null, int? durationMs = null)
        => Add(message, title, ToastSeverity.Info, durationMs ?? DefaultDurationMs);

    public IReadOnlyList<ToastMessage> GetActiveToasts()
    {
        lock (_lock)
            return _toasts.ToList().AsReadOnly();
    }

    public void DismissToast(Guid id)
    {
        lock (_lock)
            _toasts.RemoveAll(t => t.Id == id);
    }

    private void Add(string message, string? title, ToastSeverity severity, int durationMs)
    {
        var toast = new ToastMessage(
            Id: Guid.NewGuid(),
            Message: message,
            Title: title,
            Severity: severity,
            DurationMs: durationMs,
            CreatedAt: DateTime.UtcNow);

        lock (_lock)
            _toasts.Add(toast);

        OnToastAdded?.Invoke(toast);

        if (durationMs > 0)
        {
            var timer = new Timer(_ => DismissToast(toast.Id), null, durationMs, Timeout.Infinite);
            _ = timer;
        }
    }

    public void Dispose()
    {
        lock (_lock)
            _toasts.Clear();
    }
}
