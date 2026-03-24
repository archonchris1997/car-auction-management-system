using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Models;

public class Suv:Vehicle
{
   public Suv(string manufacturer, string model, int year, double startingBid, int numberOfSeats)
      : base(VehicleType.Suv, manufacturer, model, year, startingBid)
   {
      NumberOfSeats = numberOfSeats;
   }

   public int NumberOfSeats { get; }

}