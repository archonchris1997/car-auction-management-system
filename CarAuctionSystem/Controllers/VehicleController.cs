using CarAuctionSystem.Dtos;
using CarAuctionSystem.Services;
using CarAuctionSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionSystem.Controllers;

public class VehicleController:ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpPost]
    public IActionResult AddVehicle([FromBody] CreateVehicleRequest vehicle)
    {
        var response = _vehicleService.AddVehicle(vehicle);
        return ToHttp(response);

    }

    [HttpGet("byManufacturer/{manufacturer}")]
    public IActionResult GetByManufacturer(string manufacturer)
    {
        var result = _vehicleService.GetByManufacturer(manufacturer);
        return ToHttp(result);
    }
 
    [HttpGet("byModel/{model}")]
    public IActionResult GetByModel(string model)
    {
        var result = _vehicleService.GetByModel(model);
        return ToHttp(result);
    }
    
    private IActionResult ToHttp<T>(OperationResult<T> result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        switch (result.ErrorType)
        {
            case ErrorType.Validation:
                return BadRequest(result);
            case ErrorType.NotFound:
                return NotFound(result);
            case ErrorType.Conflict:
                return Conflict(result);
            default:
                return StatusCode(500, result);
        }
    }
}