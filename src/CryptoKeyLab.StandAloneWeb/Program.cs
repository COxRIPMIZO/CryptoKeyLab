using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.StandAloneWeb;
using CryptoKeyLab.StandAloneWeb.Core.Factories;
using CryptoKeyLab.StandAloneWeb.CryptoKeyLab.StandAloneWeb.Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//1.register the algorithm registry service
builder.Services.AddScoped<AlgorithmRegistry>();

//2. register the algorithms classes 
builder.Services.AddScoped<IHashFactory, HashFactory>();
builder.Services.AddScoped<IEncodingFactory, EncodingFactory>();

//await builder.Build().RunAsync();
var host = builder.Build();

var getAlgorithmRegistry = host.Services.GetRequiredService<AlgorithmRegistry>();

await getAlgorithmRegistry.InitializeAsync();

await host.RunAsync();
