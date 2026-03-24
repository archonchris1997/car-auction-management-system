using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Truck : Vehicle
{
    public Truck(string manufacturer, string model, int year, double startingBid, int loadCapacity)
        : base(VehicleType.Truck, manufacturer, model, year, startingBid)
    {
        LoadCapacity = loadCapacity;
    }

    public int LoadCapacity { get; }

}