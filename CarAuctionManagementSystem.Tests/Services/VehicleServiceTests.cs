namespace CarAuctionManagementSystem.Tests.Services;

using CarAuctionSystem.Dtos;
using CarAuctionSystem.Factories;
using CarAuctionSystem.Models;
using CarAuctionSystem.Repository;
using CarAuctionSystem.Services;
using CarAuctionSystem.Utils;
using CarAuctionSystem.Validation;
using Moq;
using Xunit;

public class VehicleServiceTests
{
    private VehicleService vehicleService;
    
    private readonly Mock<IVehicleRepository> _mockRepo;
    private readonly Mock<ICreateVehicleValidator> _mockValidator;
    private readonly Mock<IVehicleFactory> _mockFactory;
    private readonly VehicleService _service;

    public VehicleServiceTests()
    {
        _mockRepo = new Mock<IVehicleRepository>();
        _mockValidator = new Mock<ICreateVehicleValidator>();
        _mockFactory = new Mock<IVehicleFactory>();
        _service = new VehicleService(_mockRepo.Object, _mockValidator.Object, _mockFactory.Object);
    }
    
    [Fact]
    public void AddVehicle_WhenValid_ReturnsSuccess_AndInsertsOnce()
    {
        //Arrange
        var vehicle = new Sedan("Toyota", "Corolla", 2021, 15000, 4);
        var request = CreateValidSedanRequest();

        _mockValidator.Setup(v => v.Validate(request)).Returns(new List<string>());
        _mockRepo.Setup(r=> r.GetById(vehicle.Id)).Returns((Vehicle?)null);
        _mockFactory.Setup(f=>f.Create(request)).Returns(vehicle);
        
        //Act
        var result = _service.AddVehicle(request);
        
        //Assert
        Assert.True(result.Success);
        Assert.Equal("Vehicle added", result.Message);
        Assert.Equal(vehicle.Id, result?.Data?.Id);
        Assert.NotNull(result?.Data);
        
    }

    [Fact]
    public void AddVehicle_WhenInvalid_ReturnsFailure()
    {
        // Arrange
        var vehicle = new Sedan("Toyota", "Corolla", 2021, 15000, 4);
        var errors = new List<string> { "Manufacturer is required" };
        
        var request = new CreateVehicleRequest
        {
            Type = VehicleType.Sedan,
            Manufacturer = "",  // inválido
            Model = "Corolla",
            Year = 2021,
            StartingBid = 15000,
            NumberOfDoors = 4
        };

        _mockValidator.Setup(v => v.Validate(request)).Returns(errors);
        
        //Act
        var result = _service.AddVehicle(request);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Validation failed", result.Message);
        Assert.Equal(ErrorType.Validation, result.ErrorType);
        Assert.Contains("Manufacturer is required", result.Errors);
        
    }

    [Fact]
    public void AddVehicle_WhenIdAlreadyExists_ReturnsConflict_AndDoesNotInsert()
    {
        //Arrange
        var vehicle = new Sedan("Toyota", "Corolla", 2021, 15000, 4);
        var request = CreateValidSedanRequest();

        _mockValidator.Setup(v => v.Validate(request)).Returns(new List<string>()); 
        _mockFactory.Setup(f => f.Create(request)).Returns(vehicle);
        
        _mockRepo.Setup(r => r.GetById(vehicle.Id)).Returns(vehicle);
        
        //Act
        var result = _service.AddVehicle(request);
        
        //Assert
        Assert.False(result.Success);
        Assert.Equal("Vehicle already exists", result.Message);
        Assert.Equal(ErrorType.Conflict, result.ErrorType);
        Assert.Contains("Id must be unique", result.Errors);
        
    }
    
    [Fact]
    public void GetByModel_WhenModelIsFound_ReturnsVehicles()
    {
        var model = "Corolla";
        var vehicles = new List<Vehicle>
        {
            new Sedan("Toyota", "Corolla", 2020, 1000, 4),
            new Sedan("Toyota", "Corolla", 2021, 1200, 4)
        };
 
        _mockRepo
            .Setup(r => r.GetByModel(model))
            .Returns(vehicles);
 
        var result = _service.GetByModel(model);
 
        Assert.True(result.Success);
        Assert.Equal("Ok", result.Message);
        Assert.Equal(2, result.Data.Count);
 
        _mockRepo.Verify(r => r.GetByModel(model), Times.Once);
    }

    [Fact]
    public void GetByModel_WhenModelDoesNotExist_ReturnsNotFound()
    {
        var model = "nope";

        _mockRepo.Setup(r => r.GetByModel(model)).Returns(new List<Vehicle>());
        
        var result = _service.GetByModel(model);
        
        Assert.False(result.Success);
        Assert.Equal("No vehicles found", result.Message);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);

    }

    // -------------------------
    // GetByManufacturer
    // -------------------------
 
    [Fact]
    public void GetByManufacturer_WhenManufacturerIsFound_ReturnsVehicles()
    {
        var manufacturer = "BMW";
        var vehicles = new List<Vehicle>
        {
            new Sedan("BMW", "M3", 2020, 20000, 4),
            new Sedan("BMW", "M5", 2021, 30000, 4)
        };
 
        _mockRepo
            .Setup(r => r.GetByManufacturer(manufacturer))
            .Returns(vehicles);
 
        var result = _service.GetByManufacturer(manufacturer);
 
        Assert.True(result.Success);
        Assert.Equal("Ok", result.Message);
        Assert.Equal(2, result.Data.Count);
 
        _mockRepo.Verify(r => r.GetByManufacturer(manufacturer), Times.Once);
    }
 
    [Fact]
    public void GetByManufacturer_WhenManufacturerDoesNotExist_ReturnsNotFound()
    {
        var manufacturer = "NonExistingManufacturer";
 
        _mockRepo
            .Setup(r => r.GetByManufacturer(manufacturer))
            .Returns(new List<Vehicle>());
 
        var result = _service.GetByManufacturer(manufacturer);
 
        Assert.False(result.Success);
        Assert.Equal(ErrorType.NotFound, result.ErrorType);
        Assert.Null(result.Data);
 
        _mockRepo.Verify(r => r.GetByManufacturer(manufacturer), Times.Once);
    }

    
    private CreateVehicleRequest CreateValidSedanRequest()
    {
        return new CreateVehicleRequest
        {
            Type = VehicleType.Sedan,
            Manufacturer = "Toyota",
            Model = "Corolla",
            Year = 2021,
            StartingBid = 15000,
            NumberOfDoors = 4
        };
    }
}