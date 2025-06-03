using AuctionSystem.Interfaces;
using AuctionSystem.Utilities.Enums;

namespace AuctionSystem.Vehicles;

public abstract class AuctionableVehicle(string manufacturer, string model, int year, decimal startingBid)
    : IVehicle, IAuctionable
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public abstract string Type { get; }
    public string Manufacturer { get; } = manufacturer;
    public string Model { get; } = model;
    public int Year { get; } = year;
    public decimal StartingBid { get; } = startingBid;
    public decimal HighestBid { get; private set; } = startingBid;
    public AuctionStatus AuctionStatus { get; private set; }


    /// <inheritdoc cref="IAuctionable"/>
    public void StartAuction()
    {
        if (AuctionStatus == AuctionStatus.Active)
        {
            throw new Exception("Auction is already active.");
        }
        if(AuctionStatus == AuctionStatus.Completed)
        {   
            throw new Exception("Auction is already completed.");
        }
        
        AuctionStatus = AuctionStatus.Active;
    }

    /// <inheritdoc cref="IAuctionable"/>
    public void PlaceBid(decimal amount)
    {
        if (AuctionStatus == AuctionStatus.Innactive)
        {
            throw new Exception("You cannot bid at this time. The Auction is not Active.");
        }
        if (AuctionStatus == AuctionStatus.Completed)
        {
            throw new Exception("You cannot bid at this time. The Auction has bee closed.");
        }
        if (amount < StartingBid || amount <= HighestBid)
        {
            throw new Exception($"The current bid is set a {HighestBid}. You must bid a higher value than that.");
        }
        HighestBid = amount;
    }

    /// <inheritdoc cref="IAuctionable"/>
    public void EndAuction()
    {
        if (AuctionStatus != AuctionStatus.Active)
        {
            throw new Exception("The Auction cannot be close since its not active.");
        }
        
        AuctionStatus = AuctionStatus.Completed;
    }
}
