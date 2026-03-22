using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Suv:Vehicle
{
   public Suv(VehicleType type, string manufacturer, string model, int year, double startingBid, int numberOfSeats) : base(type, manufacturer, model, year, startingBid)
   {
      NumberOfSeats = numberOfSeats;
   }

   public int NumberOfSeats { get; }

}