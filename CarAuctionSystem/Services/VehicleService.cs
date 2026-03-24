using CarAuctionSystem.Dtos;
using CarAuctionSystem.Factories;
using CarAuctionSystem.Mappers;
using CarAuctionSystem.Models;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Utils;
using CarAuctionSystem.Validation;

namespace CarAuctionSystem.Services;

public class VehicleService:IVehicleService
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


    public OperationResult<VehicleDto> AddVehicle(CreateVehicleRequest request)
    {
        var errors = _validator.Validate(request);
        if (errors.Count > 0)
        {
            return new OperationResult<VehicleDto>
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors,
                ErrorType = ErrorType.Validation,
                Data = null
            };
        }
 
        var vehicle = _factory.Create(request);
 
        if (_repository.GetById(vehicle.Id) != null)
        {
            return new OperationResult<VehicleDto>
            {
                Success = false,
                Message = "Vehicle already exists",
                Errors = new List<string> { "Id must be unique" },
                ErrorType = ErrorType.Conflict,
                Data = null
            };
        }
 
        _repository.Insert(vehicle);
 
        return new OperationResult<VehicleDto>
        {
            Success = true,
            Message = "Vehicle added",
            Data = VehicleMapper.ConvertToDto(vehicle)
        };
    }

    

    public OperationResult<List<VehicleDto>> GetByManufacturer(string manufacturer)
    {
        var vehicles = _repository.GetByManufacturer(manufacturer);
 
        if (!vehicles.Any())
            return new OperationResult<List<VehicleDto>>
            {
                Success = false,
                Message = "No vehicles found",
                ErrorType = ErrorType.NotFound,
                Data = null
            };
 
        var dtos = new List<VehicleDto>();
        foreach (var vehicle in vehicles)
        {
            var dto = VehicleMapper.ConvertToDto(vehicle);
            dtos.Add(dto);
        }
 
        return new OperationResult<List<VehicleDto>>
        {
            Success = true,
            Message = "Ok",
            Data = dtos
        };
    }
    

    public OperationResult<List<VehicleDto>> GetByModel(string model)
    {
        var vehicles = _repository.GetByModel(model);
 
        if (!vehicles.Any())
            return new OperationResult<List<VehicleDto>>
            {
                Success = false,
                Message = "No vehicles found",
                ErrorType = ErrorType.NotFound,
                Data = null
            };
 
        var dtos = new List<VehicleDto>();
        foreach (var vehicle in vehicles)
        {
            var dto = VehicleMapper.ConvertToDto(vehicle);
            dtos.Add(dto);
        }
 
        return new OperationResult<List<VehicleDto>>
        {
            Success = true,
            Message = "Ok",
            Data = dtos
        };
    }

    public OperationResult<List<VehicleDto>> GetByYear(int year)
    {
        var vehicles = _repository.GetByYear(year);

        if (!vehicles.Any())
        {
            return new OperationResult<List<VehicleDto>>
            {
                    Success = false,
                    Message = "No vehicles found",
                    ErrorType = ErrorType.NotFound,
                    Data = null
            };
            
        }

        var dtos = new List<VehicleDto>();
        
        foreach (var vehicle in vehicles)
        {
            var dto = VehicleMapper.ConvertToDto(vehicle);
            dtos.Add(dto);
        }
 
        return new OperationResult<List<VehicleDto>>
        {
            Success = true,
            Message = "Ok",
            Data = dtos
        };


    }
}