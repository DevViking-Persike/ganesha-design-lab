using Ganesha.DesignLab.Shared.Models.Feedback;
using Microsoft.AspNetCore.Components;

namespace Ganesha.DesignLab.Shared.Abstractions.Services;

public interface IModalService
{
    bool IsOpen { get; }
    event Action? OnStateChanged;
    void Open(RenderFragment content, ModalOptions? options = null);
    void Close();
}
