using CarAuctionSystem.Factories;
using CarAuctionSystem.Mappers;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Services;
using CarAuctionSystem.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Repository
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();

// Factory
builder.Services.AddScoped<IVehicleFactory, VehicleFactory>();

// Validator
builder.Services.AddScoped<ICreateVehicleValidator, CreateVehicleValidator>();

// Services
builder.Services.AddScoped<VehicleService>();

var app = builder.Build();

app.MapControllers();
app.Run();