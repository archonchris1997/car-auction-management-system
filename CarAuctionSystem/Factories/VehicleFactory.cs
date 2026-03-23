using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;
using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Factories;

public class VehicleFactory : IVehicleFactory
{
    public Vehicle Create(CreateVehicleRequest request)
    {
        return request.Type switch
        {
            VehicleType.Sedan => new Sedan(
                VehicleType.Sedan,          // adicionar este
                request.Manufacturer,
                request.Model,
                request.Year,
                request.StartingBid,
                request.NumberOfDoors!.Value),

            VehicleType.Hatchback => new Hatchback(
                VehicleType.Hatchback,      // adicionar este
                request.Manufacturer,
                request.Model,
                request.Year,
                request.StartingBid,
                request.NumberOfDoors!.Value),

            VehicleType.Suv => new Suv(
                VehicleType.Suv,            // adicionar este
                request.Manufacturer,
                request.Model,
                request.Year,
                request.StartingBid,
                request.NumberOfSeats!.Value),

            VehicleType.Truck => new Truck(
                VehicleType.Truck,          // adicionar este
                request.Manufacturer,
                request.Model,
                request.Year,
                request.StartingBid,
                request.LoadCapacity!.Value),
 
            _ => throw new InvalidOperationException($"Unsupported vehicle type: {request.Type}")
        };
    }
}