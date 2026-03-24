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
        var vehicle = _vehicleRepository.GetById(vehicleId);
        if (vehicle == null)
        {
            return new OperationResult<AuctionDto>()
            {
                Success = false,
                Message = "Vehicle does not exist",
                Errors = new List<string> { "Vehicle not found" },
                ErrorType = ErrorType.NotFound,
                Data = null
            };
        }
        
        var auction = _auctionRepository.GetByVehicleId(vehicleId);
        if (auction != null)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Vehicle already has an auction",
                Errors = new List<string> { "Vehicle already has an auction" },
                ErrorType = ErrorType.Conflict,
                Data = null
            };
                
        }
        
        auction.Active =  true;
        _auctionRepository.Insert(auction);
        
        return new OperationResult<AuctionDto>
        {
            Success = true,
            Message = "Auction started",
            Data = AuctionMapper.ConvertToDto(auction)
        };
        
        
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