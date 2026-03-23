using CarAuctionSystem.Dtos;
using CarAuctionSystem.Utils;

namespace CarAuctionSystem.Validation;

public class CreateVehicleValidator : ICreateVehicleValidator
{
    public List<string> Validate(CreateVehicleRequest request)
    {
        var errors = new List<string>();
 
        if (request == null)
        {
            errors.Add("Request is required");
            return errors;
        }
 
        if (string.IsNullOrWhiteSpace(request.Manufacturer))
            errors.Add("Manufacturer is required");
 
        if (string.IsNullOrWhiteSpace(request.Model))
            errors.Add("Model is required");
 
        if (request.Year <= 0)
            errors.Add("Year must be valid");
 
        if (request.StartingBid < 0)
            errors.Add("StartingBid must be greater than or equal to zero");
 
        switch (request.Type)
        {
            case VehicleType.Sedan:
                if (request.NumberOfDoors == null || request.NumberOfDoors <= 0)
                    errors.Add("NumberOfDoors is required for Sedan");
                break;
 
            case VehicleType.Hatchback:
                if (request.NumberOfDoors == null || request.NumberOfDoors <= 0)
                    errors.Add("NumberOfDoors is required for Hatchback");
                break;
 
            case VehicleType.Suv:
                if (request.NumberOfSeats == null || request.NumberOfSeats <= 0)
                    errors.Add("NumberOfSeats is required for SUV");
                break;
 
            case VehicleType.Truck:
                if (request.LoadCapacity == null || request.LoadCapacity <= 0)
                    errors.Add("LoadCapacity is required for Truck");
                break;
 
            default:
                errors.Add("Vehicle type is not supported");
                break;
        }
 
        return errors;
    }


}