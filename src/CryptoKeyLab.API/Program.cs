using CryptoKeyLab.API.Filters;
using CryptoKeyLab.API.Middleware.Exceptions;
using CryptoKeyLab.Core.Factories;
using CryptoKeyLab.Core.Services;
using CryptoKeyLab.Core.Services.InternalCode.DependencyInjection;
using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Domain.Interfaces.Cryptography.Hash;
using CryptoKeyLab.Domain.Interfaces.Factories;
using CryptoKeyLab.Infrastructure.Repositories;
using CryptoKeyLab.Infrastructure.Repositories.Cryptography.HashMetaData;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
//builder.Services.AddScoped<IHashAlgorithm, Sha3_512Algorithm>();
//builder.Services.AddScoped<ISystemHashProvider, SystemHashProvider>();

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

app.Run();
