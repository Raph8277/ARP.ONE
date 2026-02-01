using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MudBlazor.ThemeManager;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddMudServices();
        // Theme state shared between layout and NavMenu component
        builder.Services.AddSingleton<ARP.ONE.BlazorApp.Client.Services.ThemeService>();
        builder.Services.AddScoped(_ => new HttpClient());

        await builder.Build().RunAsync();
    }
}
