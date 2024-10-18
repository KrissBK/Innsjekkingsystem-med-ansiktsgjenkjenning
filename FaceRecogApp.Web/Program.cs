using FaceRecogApp.Web;
using FaceRecogApp.Web.Interfaces;
using FaceRecogApp.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string baseAddress;

if (builder.HostEnvironment.IsDevelopment())
{
    baseAddress = builder.Configuration.GetValue<string>("BaseUrlDev");
} else
{
    baseAddress = builder.Configuration.GetValue<string>("BaseUrl");
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<IFaceService, FaceService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
