namespace AuctionSystem.Interfaces;

public interface IVehicle
{
    Guid Id { get; }
    string Type { get; }
    string Manufacturer { get; }
    string Model { get; }
    int Year { get; }
    decimal StartingBid { get; }
}