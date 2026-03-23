using CarAuctionSystem.Dtos;

namespace CarAuctionSystem.Validation;

public interface ICreateVehicleValidator
{
    List<string> Validate(CreateVehicleRequest request);

}