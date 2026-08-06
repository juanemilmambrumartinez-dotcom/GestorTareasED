using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GestorTareasED.Web;
using GestorTareasED.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5272")
});

builder.Services.AddScoped<ProjectApiService>();
builder.Services.AddScoped<TaskApiService>();

await builder.Build().RunAsync();