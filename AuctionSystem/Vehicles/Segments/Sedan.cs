namespace AuctionSystem.Vehicles.Segments;

public class Sedan(string manufacturer, string model, int year, decimal startingBid, int numberOfDoors)
    : AuctionableVehicle(manufacturer, model, year, startingBid)
{
    public int NumberOfDoors  {get; set;} = numberOfDoors;
    public override string Type =>"Sedan";
}