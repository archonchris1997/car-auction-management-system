using CarAuctionSystem.Dtos;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Services;

public class AuctionService:IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IVehicleRepository _vehicleRepository;

    public AuctionService(IAuctionRepository auctionRepository, IVehicleRepository vehicleRepository)
    {
        _auctionRepository = auctionRepository;
        _vehicleRepository = vehicleRepository;
        
    }
    
    public OperationResult<AuctionDto> StartAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }

    public OperationResult<AuctionDto> CloseAuction(Guid vehicleId)
    {
        throw new NotImplementedException();
    }

    public OperationResult<AuctionDto> PlaceBid(Guid vehicleId, int bid)
    {
        throw new NotImplementedException();
    }
}