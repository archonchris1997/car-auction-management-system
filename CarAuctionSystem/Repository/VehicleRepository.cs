using CarAuctionSystem.Models;

namespace CarAuctionSystem.Repository;

public class VehicleRepository : IVehicleRepository
{
    List<Vehicle> _vehicles;
    
    public Vehicle? GetById(Guid id)
    {
        return _vehicles.FirstOrDefault(x => x.Id == id);
    }

    public List<Vehicle> GetByModel(string model)
    {
        return _vehicles.Where(x => x.Model == model).ToList();
    }

    public List<Vehicle> GetByManufacturer(string manufacturer)
    {
        return _vehicles.Where(x => x.Manufacturer == manufacturer).ToList();
    }

    public void Insert(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
    }
}