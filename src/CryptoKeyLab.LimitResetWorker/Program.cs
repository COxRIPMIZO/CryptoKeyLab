using CryptoKeyLab.LimitResetWorker;
using CryptoKeyLab.LimitResetWorker.Infra;
using CryptoKeyLab.LimitResetWorker.Interfaces;
using CryptoKeyLab.LimitResetWorker.Models;
using CryptoKeyLab.LimitResetWorker.Services;
using CryptoKeyLab.LimitResetWorker.Services.Shared;
using Serilog;
using System.Reflection;


#region register services as a windows service

Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)! ?? Environment.CurrentDirectory;

if (args is { Length: 1 })
{
    await RegisterServices.ResgisterAsWindowsService(args);
    return;
}

#endregion


var builder = Host.CreateApplicationBuilder(args);

// 👉 THE MAGIC LINE: Tells .NET to communicate with Windows SCM
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "CryptoKeyLab.LimitResetWorker";
});

builder.Services.AddHostedService<LimitResetWorker>();

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

//register for api key reset utility repo
builder.Services.AddTransient<IApiKeyResetRepo, ApiKeyResetRepo>();

//register for api key reset utility service
builder.Services.AddTransient<IApiKeyResetService, ApiKeyResetService>();

var host = builder.Build();

#region register serilog for logging
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

Serilog.Debugging.SelfLog.Enable(msg => Log.Error("Serilog SelfLog: {Message}", msg));

#endregion

host.Run();
