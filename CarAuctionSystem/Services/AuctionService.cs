using CarAuctionSystem.Dtos;
using CarAuctionSystem.Mappers;
using CarAuctionSystem.Models;
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
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Vehicle does not exist",
                Errors = new List<string> { "Vehicle not found" },
                ErrorType = ErrorType.NotFound,
                Data = null
            };
        }
 
        var existingAuction = _auctionRepository.GetByVehicleId(vehicleId);
        if (existingAuction != null)
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
 
        var auction = new Auction(vehicle);
        auction.Active = true;
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
        var vehicle = _vehicleRepository.GetById(vehicleId);
        if (vehicle == null)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Vehicle does not exist",
                Errors = new List<string> { "Vehicle not found" },
                ErrorType = ErrorType.NotFound,
                Data = null
            };
        }
 
        var auction = _auctionRepository.GetByVehicleId(vehicleId);
        if (auction == null)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Auction does not exist",
                Errors = new List<string> { "Auction not found" },
                ErrorType = ErrorType.NotFound,
                Data = null
            };
        }
 
        if (auction.Active == false)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Auction is already closed",
                Errors = new List<string> { "Auction is already closed" },
                ErrorType = ErrorType.Conflict,
                Data = null
            };
        }
 
        auction.Active = false;
        _auctionRepository.Update(auction);
 
        return new OperationResult<AuctionDto>
        {
            Success = true,
            Message = "Auction closed",
            Data = AuctionMapper.ConvertToDto(auction)
        };
    }

    public OperationResult<AuctionDto> PlaceBid(Guid vehicleId, int bid)
    {
        var auction = _auctionRepository.GetByVehicleId(vehicleId);
        if (auction == null)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Auction does not exist",
                Errors = new List<string> { "Auction not found" },
                ErrorType = ErrorType.NotFound,
                Data = null
            };
        }
 
        if (auction.Active == false)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Auction is not active",
                Errors = new List<string> { "Cannot place a bid on a closed auction" },
                ErrorType = ErrorType.Conflict,
                Data = null
            };
        }
 
        if (bid <= auction.CurrentBid)
        {
            return new OperationResult<AuctionDto>
            {
                Success = false,
                Message = "Bid is too low",
                Errors = new List<string> { "Bid must be higher than current bid" },
                ErrorType = ErrorType.Validation,
                Data = null
            };
        }
 
        auction.CurrentBid = bid;
        _auctionRepository.Update(auction);
 
        return new OperationResult<AuctionDto>
        {
            Success = true,
            Message = "Bid placed successfully",
            Data = AuctionMapper.ConvertToDto(auction)
        };
    }
}