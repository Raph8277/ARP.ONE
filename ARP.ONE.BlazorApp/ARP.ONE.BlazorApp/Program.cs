using System.IO;
using MudBlazor.Services;
using ARP.ONE.BlazorApp.Client.Pages;
using ARP.ONE.BlazorApp.Components;
using ARP.ONE.BlazorApp.Client.Services;
using ARP.ONE.BlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();
// Note: Theme manager components are used in the client; avoid calling AddMudThemeManager here to prevent missing-extension issues

// Register ThemeService for server-side DI so shared components can inject it during prerender
builder.Services.AddSingleton<ThemeService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient());

var themeDbPath = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", "ARP.ONE.Data", "ARP.themes.sqlite"));
builder.Services.AddSingleton(new ThemeRepository(themeDbPath));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();
await app.Services.GetRequiredService<ThemeRepository>().EnsureCreatedAsync();

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

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ARP.ONE.BlazorApp.Client._Imports).Assembly);

app.MapGet("/api/themes", async (ThemeRepository repo) => await repo.GetNamesAsync());

app.MapGet("/api/themes/{name}", async (ThemeRepository repo, string name) =>
{
    var theme = await repo.GetByNameAsync(name);
    return theme is null ? Results.NotFound() : Results.Ok(new ThemeDto(theme.Name, theme.Json));
});

app.MapPost("/api/themes", async (ThemeRepository repo, ThemeSaveRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Json))
    {
        return Results.BadRequest();
    }

    await repo.UpsertAsync(request.Name.Trim(), request.Json);
    return Results.NoContent();
});

app.MapDelete("/api/themes/{name}", async (ThemeRepository repo, string name) =>
{
    var deleted = await repo.DeleteAsync(name);
    return deleted ? Results.NoContent() : Results.NotFound();
});

app.Run();

record ThemeSaveRequest(string Name, string Json);
record ThemeDto(string Name, string Json);
