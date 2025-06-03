using AuctionSystem.Interfaces;

namespace AuctionSystem.Core;

public class AuctionInventory
{
    private Dictionary<Guid, IVehicle> _vehicles = new Dictionary<Guid, IVehicle>();

    /// <summary>
    ///     Adds a vehicle to the Inventory
    /// </summary>
    /// <param name="vehicle">Vehicle to add</param>
    /// <param name="isBatch">If this method is being called by AddVehicles, this param is set to true to write a different msg in the console</param>
    /// <exception cref="Exception">If vehicle already exists in the inventory</exception>
    public void AddVehicle(IVehicle vehicle, bool isBatch = false)
    {
        if (_vehicles.ContainsKey(vehicle.Id))
        {
            throw new Exception("Vehicle already exists.");
        }
        _vehicles.Add(vehicle.Id, vehicle);
        if (!isBatch)
        {
            Console.WriteLine($"Vehicle of Type {vehicle.Type} with ID {vehicle.Id} has been added to the inventory. Total Inventory size: {_vehicles.Count}");
        }
    }

    /// <summary>
    ///     Add multiple vehicles to inventory
    /// </summary>
    /// <param name="vehiclesToAdd">Vehicles to add to inventory</param>
    public void AddVehicles(params IVehicle[] vehiclesToAdd)
    {
        foreach (var vehicle in vehiclesToAdd)
        {
            AddVehicle(vehicle, true);
        }
        Console.WriteLine($"{vehiclesToAdd.Length} vehicles added to the2 inventory. Total Inventory size: {_vehicles.Count}");
    }

    /// <summary>
    ///     Get vehicles by type, manufacturer, model and/or year
    /// </summary>
    /// <param name="type">vehicle type</param>
    /// <param name="manufacturer">vehicle manufacturer</param>
    /// <param name="model">vehicle model</param>
    /// <param name="year">vehicle year</param>
    /// <returns>A list of vehicles that match the search criteria</returns>
    public List<IVehicle> GetVehicles(string? type = null, string? manufacturer = null, string? model = null, int? year = null)
    {
        
        return _vehicles.Values.Where(x=> 
            (type == null || x.Type.Equals(type, StringComparison.OrdinalIgnoreCase)) 
            && (manufacturer == null ||x.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)) 
            && (model == null || model.Equals(x.Model, StringComparison.OrdinalIgnoreCase))
            && (year == null || x.Year == year)
            ).ToList();
    }

    /// <summary>
    ///     Gets a vehicle from the inventory for a specific ID
    /// </summary>
    /// <param name="id">Vehicle ID</param>
    /// <returns>The vehicle whose ID matches the input</returns>
    public IVehicle? GetVehicleById(Guid id)
    {
        return _vehicles.TryGetValue(id, out IVehicle vehicle) ? vehicle : null;
    }

    /// <summary>
    ///     Gets the ammount of cars present in the inventory
    /// </summary>
    /// <returns>Nr of cars in the inventory</returns>
    public int GetInventorySize()
    {
        return _vehicles.Count;
    }
}