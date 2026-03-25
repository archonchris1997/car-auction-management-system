using CarAuctionSystem.Factories;
using CarAuctionSystem.Mappers;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Services;
using CarAuctionSystem.Validation;

var builder = WebApplication.CreateBuilder(args);

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

app.MapControllers();
app.Run();