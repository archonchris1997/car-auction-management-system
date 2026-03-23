using CarAuctionSystem.Factories;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Validation;

namespace CarAuctionSystem.Services;

public class VehicleService
{
    private readonly IVehicleRepository _repository;
    private readonly ICreateVehicleValidator _validator;
    private readonly IVehicleFactory _factory;
    
    public VehicleService(
        IVehicleRepository repository,
        ICreateVehicleValidator validator,
        IVehicleFactory factory)
    {
        _repository = repository;
        _validator = validator;
        _factory = factory;
    }
    
}