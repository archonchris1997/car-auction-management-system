using CarAuctionSystem.Models;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Services;
using CarAuctionSystem.Utils;
using Moq;

namespace CarAuctionManagementSystem.Tests.Services;

public class AuctionServiceTest
{
    private readonly IAuctionService _auctionService;
    private readonly Mock<IVehicleRepository> _mockVehicleRepo;
    private readonly Mock<IAuctionRepository> _mockAuctionRepo;
    
    
    public AuctionServiceTest()
    {
        _mockVehicleRepo = new Mock<IVehicleRepository>();
        _mockAuctionRepo = new Mock<IAuctionRepository>();
        
        _auctionService = new AuctionService(_mockAuctionRepo.Object,_mockVehicleRepo.Object);
    }
    
    // -------------------------
    // StartAuction
    // -------------------------

    [Fact]
    public void StartAuction_WhenVehicleDoesNotExist_ReturnsFailure()
    {
        //Arrange
        var vehicleId = Guid.NewGuid();
        _mockVehicleRepo.Setup(r=>r.GetById(vehicleId)).Returns((Vehicle?)null);
        
        //Act
        var result = _auctionService.StartAuction(vehicleId);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Vehicle does not exist", result.Message);
        Assert.Equal(ErrorType.NotFound,result.ErrorType);
        
    }

    [Fact]
    public void StartAuction_WhenVehicleExistsAndNoAuction_ReturnsSuccess()
    {
        //Arrange
        var vehicleId = Guid.NewGuid();
        
        var vehicle = new Sedan("Toyota", "Corolla", 2020, 1000, 4);
 
        
        _mockVehicleRepo.Setup(r => r.GetById(vehicleId)).Returns(vehicle);
        _mockAuctionRepo.Setup(a => a.GetByVehicleId(vehicleId)).Returns((Auction?)null);
        
        //Act
        var result = _auctionService.StartAuction(vehicleId);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal("Auction started", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Active);
        Assert.Equal(vehicle.Id, result.Data.VehicleId);
        
    }

}