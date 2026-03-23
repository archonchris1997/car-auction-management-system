using CarAuctionSystem.Models;

namespace CarAuctionSystem.Repository;

public interface IVehicleRepository
{
    Vehicle? GetById(Guid id);
    List<Vehicle> GetByModel(string model);
    List<Vehicle> GetByManufacturer(string manufacturer);
    void Insert(Vehicle vehicle);
}