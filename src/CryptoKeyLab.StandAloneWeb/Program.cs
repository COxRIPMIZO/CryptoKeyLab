using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.StandAloneWeb;
using CryptoKeyLab.StandAloneWeb.Core.Factories;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Core.Services;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//1.register the algorithm registry service
// We use AddSingleton so the parsed data stays in memory forever
builder.Services.AddScoped<IAlgorithmRegistry, AlgorithmRegistry>();

//2. register the algorithms classes 
builder.Services.AddScoped<IHashFactory, HashFactory>();
builder.Services.AddScoped<IEncodingFactory, EncodingFactory>();

builder.Services.AddMudServices();

//await builder.Build().RunAsync();
var host = builder.Build();

//var getAlgorithmRegistry = host.Services.GetRequiredService<AlgorithmRegistry>();
var getAlgorithmRegistry = host.Services.GetRequiredService<IAlgorithmRegistry>();


await getAlgorithmRegistry.InitializeAsync();

await host.RunAsync();
