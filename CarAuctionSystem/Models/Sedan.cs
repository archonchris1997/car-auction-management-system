using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Sedan : Vehicle
{
    public Sedan(string manufacturer, string model, int year, double startingBid, int numberOfDoors)
        : base(VehicleType.Sedan, manufacturer, model, year, startingBid)
    {
        NumberOfDoors = numberOfDoors;
    }

    public int NumberOfDoors { get; set; }
    
    
}