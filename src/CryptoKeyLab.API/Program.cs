using CryptoKeyLab.API.Filters;
using CryptoKeyLab.Core.Services;
using CryptoKeyLab.Domain.Interfaces;
using CryptoKeyLab.Infrastructure.Repositories;
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

#endregion

var app = builder.Build();

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
