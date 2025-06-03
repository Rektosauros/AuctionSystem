using AuctionSystem.Core;
using AuctionSystem.Utilities.Enums;
using AuctionSystem.Vehicles.Segments;
using FluentAssertions;

namespace AuctionSystem.Tests;

public class AuctionServiceTests
{
    private readonly AuctionInventory _auctionInventory;
    private readonly AuctionService _auctionService;

    public AuctionServiceTests()
    {
        _auctionInventory = new AuctionInventory();
        _auctionService = new AuctionService(_auctionInventory);
    }

    [Fact]
    public void AuctionServiceTests_StartAuction_ShouldChangeStatusToActive()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);

        hatch1.AuctionStatus.Should().Be(AuctionStatus.Innactive);
        
        //Act
        _auctionService.StartAuction(hatch1.Id);
        
        //Assert
        hatch1.AuctionStatus.Should().Be(AuctionStatus.Active);
    }
    
    [Fact]
    public void AuctionServiceTests_EndAuction_ShouldChangeStatusToCompleted()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);

        hatch1.AuctionStatus.Should().Be(AuctionStatus.Innactive);
        
        _auctionService.StartAuction(hatch1.Id);
        hatch1.AuctionStatus.Should().Be(AuctionStatus.Active);
        
        //Act
        _auctionService.EndAuction(hatch1.Id);
        
        //Assert
        hatch1.AuctionStatus.Should().Be(AuctionStatus.Completed);
    }
    
    [Fact]
    public void AuctionServiceTests_PlaceBid_ShouldUpdateHighestBid()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        hatch1.AuctionStatus.Should().Be(AuctionStatus.Innactive);
        
        _auctionService.StartAuction(hatch1.Id);
        hatch1.AuctionStatus.Should().Be(AuctionStatus.Active);
        hatch1.HighestBid.Should().Be(10000);
        
        //Act
        _auctionService.PlaceBid(hatch1.Id, 14000);
        
        //Assert
        hatch1.HighestBid.Should().Be(14000);
    }

    [Fact]
    public void AuctionServiceTests_StartAuction_AuctionAlreadyActive_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);

        _auctionService.StartAuction(hatch1.Id); 
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.StartAuction(hatch1.Id));
        
        //Assert
        exception.Message.Should().Be("Auction is already active.");
    }
    
    [Fact]
    public void AuctionServiceTests_StartAuction_AuctionAlreadyCompleted_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);

        _auctionService.StartAuction(hatch1.Id); 
        _auctionService.EndAuction(hatch1.Id);
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.StartAuction(hatch1.Id));
        
        //Assert
        exception.Message.Should().Be("Auction is already completed.");
    }
    
    [Fact]
    public void AuctionServiceTests_PlaceBid_AuctionInnactive_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.PlaceBid(hatch1.Id,12000));
        
        //Assert
        exception.Message.Should().Be("You cannot bid at this time. The Auction is not Active.");
    }
    
    [Fact]
    public void AuctionServiceTests_PlaceBid_AuctionCompleted_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        _auctionService.StartAuction(hatch1.Id);
        _auctionService.EndAuction(hatch1.Id);
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.PlaceBid(hatch1.Id,12000));
        
        //Assert
        exception.Message.Should().Be("You cannot bid at this time. The Auction has bee closed.");
    }
    
    [Fact]
    public void AuctionServiceTests_PlaceBid_BidBellowCurrentHighestBid_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        _auctionService.StartAuction(hatch1.Id);
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.PlaceBid(hatch1.Id,5000));
        
        //Assert
        exception.Message.Should().Be("The current bid is set a 10000. You must bid a higher value than that.");
    }
    
    [Fact]
    public void AuctionServiceTests_PlaceBid_BidEqualToCurrentHighestBid_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        _auctionService.StartAuction(hatch1.Id);
        
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.PlaceBid(hatch1.Id,10000));
        
        //Assert
        exception.Message.Should().Be("The current bid is set a 10000. You must bid a higher value than that.");
    }
    
    [Fact]
    public void AuctionServiceTests_EndAuction_AuctionNotStarted_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
                
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.EndAuction(hatch1.Id));
        
        //Assert
        exception.Message.Should().Be("The Auction cannot be close since its not active.");
    }
    
    [Fact]
    public void AuctionServiceTests_EndAuction_AuctionAlreadyCompleted_ShouldThrowException()
    {
        //Arrange
        var hatch1 = new Hatchback("Skoda", "Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        _auctionService.StartAuction(hatch1.Id);
        _auctionService.EndAuction(hatch1.Id);
                
        //Act
        var exception = Assert.Throws<Exception>(()=>_auctionService.EndAuction(hatch1.Id));
        
        //Assert
        exception.Message.Should().Be("The Auction cannot be close since its not active.");
    }
    
}