using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DoodleSync.Client;
using DoodleSync.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Configure logging
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Register services
builder.Services.AddSingleton<EventStore>();
builder.Services.AddScoped<ISignalRService, SignalRService>();

// Build and run the application
await builder.Build().RunAsync();
