using CryptoKeyLab.LimitResetWorker;
using CryptoKeyLab.LimitResetWorker.Infra;
using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using CryptoKeyLab.LimitResetWorker.Services;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//inject the appseeting.json data into the DI container so that it can be used in the worker class
builder.Services.Configure<ResetWorkerSettings>(builder.Configuration.GetSection("ResetWorkerSettings"));

// =====================================================================
// 2. DEPENDENCY INJECTION REGISTRATIONS
// We use AddTransient so fresh instances are created when the worker loops
// =====================================================================

//// Register the Repository (Dapper/SQL layer)
builder.Services.AddTransient<IApiKeyMetaData, ApiKeyMetaData>();

// Register the Service (Business Logic layer)
builder.Services.AddTransient<IApiKeyGetService, ApiKeyGetService>();

var host = builder.Build();
host.Run();
