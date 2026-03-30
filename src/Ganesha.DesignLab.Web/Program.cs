using Ganesha.DesignLab.Web.Components;
using Ganesha.DesignLab.Shared.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add Ganesha Design Lab services (theme, toast, drawer, modal)
builder.Services.AddGaneshaDesignLab();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Ganesha.DesignLab.Shared._Imports).Assembly);

app.Run();
