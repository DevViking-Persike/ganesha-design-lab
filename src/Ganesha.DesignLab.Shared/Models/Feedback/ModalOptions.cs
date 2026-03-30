namespace Ganesha.DesignLab.Shared.Models.Feedback;

public record ModalOptions(
    string? Title = null,
    bool ShowCloseButton = true,
    bool CloseOnBackdropClick = true,
    string Size = "md");
