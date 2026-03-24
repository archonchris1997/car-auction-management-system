using CarAuctionSystem.Dtos;
using CarAuctionSystem.Models;
using CarAuctionSystem.Services;
using CarAuctionSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionSystem.Controllers;

[ApiController]
[Route("api/controller")]
public class AuctionController:ControllerBase
{
    private readonly IAuctionService _auctionService;
    
    public AuctionController(IAuctionService service)
    {
        _auctionService = service;
    }

    [HttpPost("startAuction/{vehicleId}")]
    public IActionResult StartAuction(Guid vehicleId)
    {
        var result = _auctionService.StartAuction(vehicleId);  
        return ToHttp(result);
    }

    [HttpPut("placeBid/{vehicleId}")]
    public IActionResult PlaceBid([FromBody] PlaceBidRequest request,Guid vehicleId)
    {
        var result = _auctionService.PlaceBid(vehicleId, request.Amount);
        return ToHttp(result);
    }

    [HttpPut("closeAuction/{vehicleId}")]
    public IActionResult CloseAuction(Guid vehicleId)
    {
        var result = _auctionService.CloseAuction(vehicleId);
        return ToHttp(result);
    }
    
    
    private IActionResult ToHttp<T>(OperationResult<T> result)
    {
        if (result.Success)
            return Ok(result);
 
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