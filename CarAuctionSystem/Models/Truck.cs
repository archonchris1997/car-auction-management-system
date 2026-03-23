using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Truck : Vehicle
{
    public Truck(VehicleType type, string manufacturer, string model, int year, double startingBid, int loadCapacity) : base(type, manufacturer, model, year, startingBid)
    {
        LoadCapacity = loadCapacity;
    }

    public int LoadCapacity { get; }

}