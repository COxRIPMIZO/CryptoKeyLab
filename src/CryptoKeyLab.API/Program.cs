using CryptoKeyLab.API.Filters;
using CryptoKeyLab.API.Middleware.Exceptions;
using CryptoKeyLab.Core.Factories;
using CryptoKeyLab.Core.Services;
using CryptoKeyLab.Core.Services.Cache;
using CryptoKeyLab.Core.Services.InternalCode.DependencyInjection;
using CryptoKeyLab.Domain.Enums;
using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Interfaces.Caching;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.Encoding;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Infrastructure.Caching;
using CryptoKeyLab.Infrastructure.Repositories;
using CryptoKeyLab.Infrastructure.Repositories.Cryptography.HashMetaData;
using CryptoKeyLab.Infrastructure.Repositories.Encoding;
using Scalar.AspNetCore;
using StackExchange.Redis;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

#region Caching Connection

builder.Services.AddCacheServices(builder.Configuration);

#endregion

#region Working and registering core services

//injecting Key Generation Service and Key Validation Service into the DI container so they can be used in the controllers and filters

// 1. Core Services
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();

//2. Repository
builder.Services.AddScoped<IApiKeyRepository, ApiKeyRepository>();

//3. Validation Service
builder.Services.AddScoped<IApiKeyValidationService, ApiKeyValidationService>();

//4.Filter(The gatekepper)
builder.Services.AddScoped<ApiKeyAuthFilter>();

//5. Hasing Factory and Algorithms
builder.Services.AddScoped<IHashFactory, HashFactory>();

//6. Hash Metadata Repository
builder.Services.AddScoped<IHashMetadataRepository, HashMetadataRepository>();

//7 register for api key hasing utility
builder.Services.AddApiHashingService();


//8. add encoder service
builder.Services.AddScoped<IEncodingMetadataRepository, EncodingMetadataRepository>();

builder.Services.AddScoped<IEncodingFactory, EncodingFactory>();

//9.add automatic  health checkup of api
builder.Services.AddHealthChecks();

#endregion

#region Register the middlewares

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

#endregion

var app = builder.Build();

//Add global exception handling , as first middleware of this pipeline
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //ENABLE SCALAR UI
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//mapping the health check endpoint to monitor the health of the application, this can be used by monitoring tools or load balancers to check if the application is running and healthy
app.MapHealthChecks("/health");

app.Run();
