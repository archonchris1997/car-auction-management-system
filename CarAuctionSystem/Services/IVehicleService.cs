using CarAuctionSystem.Dtos;
using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Services;

public interface IVehicleService
{
    OperationResult<VehicleDto> AddVehicle(CreateVehicleRequest request);

    OperationResult<List<VehicleDto>> GetAll();
    OperationResult<List<VehicleDto>> GetByManufacturer(string manufacturer);
    OperationResult<List<VehicleDto>> GetByModel(string model);
    OperationResult<List<VehicleDto>> GetByYear(int year);
}
}