using Ganesha.DesignLab.Shared.Models.Feedback;
using Microsoft.AspNetCore.Components;

namespace Ganesha.DesignLab.Shared.Abstractions.Services;

public interface IDrawerService
{
    bool IsOpen { get; }
    event Action? OnStateChanged;
    void Open(RenderFragment content, DrawerPosition position = DrawerPosition.Right, string? title = null);
    void Close();
}
