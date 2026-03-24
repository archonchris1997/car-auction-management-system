using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;

namespace CarAuctionSystem.Mappers;

public class AuctionMapper
{
    public static AuctionDto ConvertToDto(Auction auction)
    {
        return new AuctionDto
        {
            Id = auction.Id,
            VehicleId = auction.Vehicle.Id,
            Active = auction.Active,
            CurrentBid = auction.CurrentBid
        };
    }
}