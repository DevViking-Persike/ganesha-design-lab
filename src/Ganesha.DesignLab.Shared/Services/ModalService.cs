using Ganesha.DesignLab.Shared.Abstractions.Services;
using Ganesha.DesignLab.Shared.Models.Feedback;
using Microsoft.AspNetCore.Components;

namespace Ganesha.DesignLab.Shared.Services;

public sealed class ModalService : IModalService
{
    public bool IsOpen { get; private set; }
    public RenderFragment? Content { get; private set; }
    public ModalOptions? Options { get; private set; }

    public event Action? OnStateChanged;

    public void Open(RenderFragment content, ModalOptions? options = null)
    {
        Content = content;
        Options = options ?? new ModalOptions();
        IsOpen = true;
        OnStateChanged?.Invoke();
    }

    public void Close()
    {
        IsOpen = false;
        Content = null;
        Options = null;
        OnStateChanged?.Invoke();
    }
}
