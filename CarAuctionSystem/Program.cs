using CarAuctionSystem.Factories;
using CarAuctionSystem.Mappers;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Services;
using CarAuctionSystem.Validation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // adiciona
builder.Services.AddSwaggerGen();             // adiciona

// Repository
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();

// Factory
builder.Services.AddScoped<IVehicleFactory, VehicleFactory>();

// Validator
builder.Services.AddScoped<ICreateVehicleValidator, CreateVehicleValidator>();

// Services
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IAuctionService, AuctionService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();