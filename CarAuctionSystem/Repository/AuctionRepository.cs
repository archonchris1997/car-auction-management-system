using CarAuctionSystem.Models;

namespace CarAuctionSystem.Repository;

public class AuctionRepository:IAuctionRepository
{
    
    
    private readonly List<Auction> _auctions = new();
 
    public Auction? GetByVehicleId(Guid vehicleId)
    {
        return _auctions.FirstOrDefault(a => a.Vehicle.Id == vehicleId);
    }
 
    public List<Auction> GetAll()
    {
        return _auctions;
    }
 
    public void Insert(Auction auction)
    {
        _auctions.Add(auction);
    }
 
    public void Update(Auction auction)
    {
        var existing = _auctions.FirstOrDefault(a => a.Id == auction.Id);
        
        if (existing != null)
        {
            existing.Active = auction.Active;
            existing.CurrentBid = auction.CurrentBid;
        }
    }
}