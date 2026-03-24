namespace CarAuctionSystem.Dtos;

public class AuctionDto
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public bool Active { get; set; }
    public double CurrentBid { get; set; }
}