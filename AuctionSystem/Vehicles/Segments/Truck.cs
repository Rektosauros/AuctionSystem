namespace AuctionSystem.Vehicles.Segments;

public class Truck(string manufacturer, string model, int year, decimal startingBid, double loadCapacity)
    : AuctionableVehicle(manufacturer, model, year, startingBid)
{
    public double LoadCapacity  {get; set;} = loadCapacity;
    public override string Type =>"Truck";
}