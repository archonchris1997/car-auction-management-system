using CarAuctionSystem.Models;

namespace CarAuctionSystem.Repository;

public interface IAuctionRepository
{
    Auction? GetByVehicleId(Guid vehicleId);
    List<Auction> GetAll();
    void Insert(Auction auction);
    void Update(Auction auction);
}