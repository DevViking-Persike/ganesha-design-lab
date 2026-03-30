using Ganesha.DesignLab.Shared.Abstractions.Services;
using Ganesha.DesignLab.Shared.Models.Feedback;
using Microsoft.AspNetCore.Components;

namespace Ganesha.DesignLab.Shared.Services;

public sealed class DrawerService : IDrawerService
{
    public bool IsOpen { get; private set; }
    public RenderFragment? Content { get; private set; }
    public DrawerPosition Position { get; private set; }
    public string? Title { get; private set; }

    public event Action? OnStateChanged;

    public void Open(RenderFragment content, DrawerPosition position = DrawerPosition.Right, string? title = null)
    {
        Content = content;
        Position = position;
        Title = title;
        IsOpen = true;
        OnStateChanged?.Invoke();
    }

    public void Close()
    {
        IsOpen = false;
        Content = null;
        Title = null;
        OnStateChanged?.Invoke();
    }
}
