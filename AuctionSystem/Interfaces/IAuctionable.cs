using AuctionSystem.Utilities.Enums;

namespace AuctionSystem.Interfaces;

public interface IAuctionable
{
    decimal HighestBid { get; }
    AuctionStatus AuctionStatus { get; }
    
    /// <summary>
    ///  Starts an auction on a vehicle.
    ///     - AuctionStatus has to be Innactive.
    /// </summary>
    void StartAuction();
    
    /// <summary>
    /// Places a bid on an active Auction.
    ///     - AuctionStatus has to be Active.
    ///     - Bid must be higher that current bid
    /// </summary>
    /// <param name="amount">amount to be placed as bid</param>
    void PlaceBid(decimal amount);
    
    /// <summary>
    /// Ends a Vehicle Auction
    ///     - AuctionStatus has to be Active.
    /// </summary>
    void EndAuction();
}