using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Hatchback : Vehicle
{
    public Hatchback(VehicleType type, string manufacturer, string model, int year, double startingBid, int numberOfDoors) : base(type, manufacturer, model, year, startingBid)
    {
        NumberOfDoors = numberOfDoors;
    }

    public int NumberOfDoors { get; set; }
    
}