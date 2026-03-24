namespace CarAuctionSystem.Models;

public class Auction
{
    public Guid Id { get; }
    public bool Active { get; set; }
    public Vehicle Vehicle { get; }
    public double CurrentBid { get; set; }
 
    public Auction(Vehicle vehicle)
    {
        Id = Guid.NewGuid();
        Vehicle = vehicle;
        CurrentBid = vehicle.StartingBid;
        Active = false;
    }
}