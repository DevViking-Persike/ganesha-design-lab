namespace Ganesha.DesignLab.Shared.Models.Forms;

public record FormFieldState(
    string Value,
    bool IsTouched,
    bool IsValid,
    string? ErrorMessage,
    bool IsDisabled);
