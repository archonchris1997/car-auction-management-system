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
    
    // -------------------------
    // CloseAuction
    // -------------------------
    
    [Fact]
    public void CloseAuction_WhenAuctionIsActive_ReturnsSuccess()
    {
        // Arrange
        var vehicle = new Sedan("Audi", "A4", 2022, 25000, 4);
        var auction = new Auction(vehicle);
        auction.Active = true;
 
        _mockVehicleRepo
            .Setup(r => r.GetById(vehicle.Id))
            .Returns(vehicle);
 
        _mockAuctionRepo
            .Setup(r => r.GetByVehicleId(vehicle.Id))
            .Returns(auction);
 
        // Act
        var result = _auctionService.CloseAuction(vehicle.Id);
 
        // Assert
        Assert.True(result.Success);
        Assert.Equal("Auction closed", result.Message);
        Assert.NotNull(result.Data);
        Assert.False(result.Data.Active);
    }

    [Fact]
    public void CloseAuction_WhenVehicleDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
 
        _mockVehicleRepo
            .Setup(r => r.GetById(vehicleId))
            .Returns((Vehicle?)null);
 
        // Act
        var result = _auctionService.CloseAuction(vehicleId);
 
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Vehicle does not exist", result.Message);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public void CloseAuction_WhenAuctionDoesNotExist_ReturnsFailure()
    {
        // Arrange
        var vehicle = new Sedan("BMW", "M3", 2020, 20000, 4);
 
        _mockVehicleRepo
            .Setup(r => r.GetById(vehicle.Id))
            .Returns(vehicle);
 
        _mockAuctionRepo
            .Setup(r => r.GetByVehicleId(vehicle.Id))
            .Returns((Auction?)null);
 
        // Act
        var result = _auctionService.CloseAuction(vehicle.Id);
 
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Auction does not exist", result.Message);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public void CloseAuction_WhenAuctionIsAlreadyClosed_ReturnsFailure()
    {
        // Arrange
        var vehicle = new Sedan("BMW", "M3", 2020, 20000, 4);
        var auction = new Auction(vehicle);
        auction.Active = false;
 
        _mockVehicleRepo
            .Setup(r => r.GetById(vehicle.Id))
            .Returns(vehicle);
 
        _mockAuctionRepo
            .Setup(r => r.GetByVehicleId(vehicle.Id))
            .Returns(auction);
 
        // Act
        var result = _auctionService.CloseAuction(vehicle.Id);
 
        // Assert
        Assert.False(result.Success);
        Assert.Equal("Auction is already closed", result.Message);
        Assert.Equal(ErrorType.Conflict, result.ErrorType);
        Assert.Null(result.Data);
    }
    
    // -------------------------
    // PlaceBid
    // -------------------------

    [Fact]
    public void PlaceBid_WhenBidIsHigherThanCurrentBid_ReturnsSuccess()
    {
        
        
        
    }


}