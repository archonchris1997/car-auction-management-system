using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Services;

public interface IAuctionService
{
    OperationResult<AuctionDto> StartAuction(Guid vehicleId);
    OperationResult<AuctionDto> CloseAuction(Guid vehicleId);
    OperationResult<AuctionDto> PlaceBid(Guid vehicleId, int bid);
}