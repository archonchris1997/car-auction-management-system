using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Dtos;

public class VehicleDto
{
    public Guid Id { get; set; }
    public VehicleType Type { get; set; }
    public string Manufacturer { get; set; } = "";
    public string Model { get; set; } = "";
    public int Year { get; set; }
    public double StartingBid { get; set; }
    public int? NumberOfDoors { get; set; }   // Sedan, Hatchback
    public int? NumberOfSeats { get; set; }   // Suv
    public int? LoadCapacity { get; set; }    // Truck
}