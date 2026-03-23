using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;

namespace CarAuctionSystem.Factories;

public interface IVehicleFactory
{
    Vehicle Create(CreateVehicleRequest request);
}