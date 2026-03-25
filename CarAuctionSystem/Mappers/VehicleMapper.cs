using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;

namespace CarAuctionSystem.Mappers;

public static class VehicleMapper
{
    public static VehicleDto ConvertToDto(Vehicle vehicle)
    {
        var dto = new VehicleDto
        {
            Id = vehicle.Id,
            Type = vehicle.Type,
            Manufacturer = vehicle.Manufacturer,
            Model = vehicle.Model,
            Year = vehicle.Year,
            StartingBid = vehicle.StartingBid
        };
 
        if (vehicle is Sedan)
        {
            var sedan = (Sedan)vehicle;
            dto.NumberOfDoors = sedan.NumberOfDoors;
        }
        else if (vehicle is Hatchback)
        {
            var hatchback = (Hatchback)vehicle;
            dto.NumberOfDoors = hatchback.NumberOfDoors;
        }
        else if (vehicle is Suv)
        {
            var suv = (Suv)vehicle;
            dto.NumberOfSeats = suv.NumberOfSeats;
        }
        else if (vehicle is Truck)
        {
            var truck = (Truck)vehicle;
            dto.LoadCapacity = truck.LoadCapacity;
        }
        else
        {
            throw new InvalidOperationException($"Unsupported vehicle type: {vehicle.GetType().Name}");
        }
 
        return dto;
    }
}