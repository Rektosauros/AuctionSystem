using AuctionSystem.Interfaces;

namespace AuctionSystem.Core;

public class AuctionService
{
    private readonly AuctionInventory _inventory;

    public AuctionService( AuctionInventory inventory )
    {
        _inventory = inventory;
    }
    
    /// <summary>
    ///     Starts an Auction for a specific vehicle
    /// </summary>
    /// <param name="id">Id of the Auctioned Vehicle</param>
    /// <exception cref="Exception">If no vehicle is found with that Id</exception>
    public void StartAuction(Guid id)
    {
        if (_inventory.GetVehicleById(id) is IAuctionable auctionable)
        {
            auctionable.StartAuction();
            Console.WriteLine($"Auction started for vehicle with ID {id}");
        }
        else
        {
            throw new Exception("The Auction cannot be started. No vehicle with that ID has been found.");
        }
    }

    /// <summary>
    ///     Places a bid on an active auction
    /// </summary>
    /// <param name="id">Id of actioned car</param>
    /// <param name="amount">bid amount to be placed</param>
    /// <exception cref="Exception">If the bid if less or equal to the current highest bid</exception>
    public void PlaceBid(Guid id, decimal amount)
    {
        if (_inventory.GetVehicleById(id) is IAuctionable auctionable)
        {
            auctionable.PlaceBid(amount);
            Console.WriteLine($"A bid of {amount}€ has been placed for the vehicle with ID {id}");
        }
        else
        {
            throw new Exception("A bid cannot be placed. No vehicle with that ID has been found.");
        }
    }

    /// <summary>
    ///     Ends a vehcile auction
    /// </summary>
    /// <param name="id">Id of auctioned vehicle</param>
    /// <exception cref="Exception">If no vehicle is found with that Id1</exception>
    public void EndAuction(Guid id)
    {
        if (_inventory.GetVehicleById(id) is IAuctionable auctionable)
        {
            auctionable.EndAuction();
            Console.WriteLine($"Auction closed for vehicle with ID {id}");
        }
        else
        {
            throw new Exception("The Auction cannot be closed. No vehicle with that ID has been found.");
        }
    }
}