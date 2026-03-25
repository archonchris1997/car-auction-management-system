using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;
using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Factories;

public class VehicleFactory : IVehicleFactory
{
    public Vehicle Create(CreateVehicleRequest request)
    {
        switch (request.Type)
        {
            case VehicleType.Sedan:
                return new Sedan(
                    request.Manufacturer,
                    request.Model,
                    request.Year,
                    request.StartingBid,
                    request.NumberOfDoors);
 
            case VehicleType.Hatchback:
                return new Hatchback(
                    request.Manufacturer,
                    request.Model,
                    request.Year,
                    request.StartingBid,
                    request.NumberOfDoors);
 
            case VehicleType.Suv:
                return new Suv(
                    request.Manufacturer,
                    request.Model,
                    request.Year,
                    request.StartingBid,
                    request.NumberOfSeats);
 
            case VehicleType.Truck:
                return new Truck(
                    request.Manufacturer,
                    request.Model,
                    request.Year,
                    request.StartingBid,
                    request.LoadCapacity);
 
            default:
                throw new InvalidOperationException($"Unsupported vehicle type: {request.Type}");
        }
    }
}