using Ganesha.DesignLab.Shared.Abstractions.Services;
using Ganesha.DesignLab.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ganesha.DesignLab.Shared.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGaneshaDesignLab(this IServiceCollection services)
    {
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<IToastService, ToastService>();
        services.AddScoped<IDrawerService, DrawerService>();
        services.AddScoped<IModalService, ModalService>();

        return services;
    }
}
