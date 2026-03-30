namespace Ganesha.DesignLab.Shared.Models.Feedback;

public record ToastMessage(
    Guid Id,
    string Message,
    string? Title,
    ToastSeverity Severity,
    int DurationMs,
    DateTime CreatedAt);
