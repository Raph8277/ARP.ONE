using MudBlazor.Services;
using ARP.ONE.BlazorApp.Client.Pages;
using ARP.ONE.BlazorApp.Components;
using ARP.ONE.BlazorApp.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();
// Note: Theme manager components are used in the client; avoid calling AddMudThemeManager here to prevent missing-extension issues

// Register ThemeService for server-side DI so shared components can inject it during prerender
builder.Services.AddSingleton<ThemeService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ARP.ONE.BlazorApp.Client._Imports).Assembly);

app.Run();
