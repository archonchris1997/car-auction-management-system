using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public abstract class Vehicle
{
    public Guid Id { get; }
    public VehicleType Type { get; }
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public double StartingBid { get; }
 
    public Vehicle(VehicleType type, string manufacturer, string model, int year, double startingBid)
    {
        Id = Guid.NewGuid();
        Type = type;
        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        StartingBid = startingBid;
    }
}