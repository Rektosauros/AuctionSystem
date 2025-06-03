namespace AuctionSystem.Vehicles.Segments;

public class SUV(string manufacturer, string model, int year, decimal startingBid, int numberOfSeats)
    : AuctionableVehicle(manufacturer, model, year, startingBid)
{
    public int NumberOfSeats  {get; set;} = numberOfSeats;
    public override string Type =>"SUV";
}