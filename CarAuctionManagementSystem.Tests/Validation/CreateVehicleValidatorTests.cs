using CarAuctionSystem.Dtos;
using CarAuctionSystem.Utils;
using CarAuctionSystem.Validation;

namespace CarAuctionManagementSystem.Tests.Validation;

public class CreateVehicleValidatorTests
{
    private readonly CreateVehicleValidator _validator = new();
    
    [Fact]
    public void Validate_WhenModelIsEmpty_ReturnsError()
    {
        var request = CreateSedanRequestWithoutModel();

        var errors = _validator.Validate(request);

        Assert.Contains("Model is required", errors);
    }
    
    [Fact]
    public void Validate_WhenSedanIsValid_ReturnsNoErrors()
    {
        var errors = _validator.Validate(ValidSedanRequest());
 
        Assert.Empty(errors);
    }
    
    private static CreateVehicleRequest ValidSedanRequest() => new()
    {
        Type = VehicleType.Sedan,
        Manufacturer = "Toyota",
        Model = "Corolla",
        Year = 2021,
        StartingBid = 15000,
        NumberOfDoors = 4
    };
    
    private static CreateVehicleRequest CreateSedanRequestWithoutModel() => new()
    {
        Type = VehicleType.Sedan,
        Manufacturer = "Toyota",
        Model = "",
        Year = 2021,
        StartingBid = 15000,
        NumberOfDoors = 4
    };
}