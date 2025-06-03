using AuctionSystem.Core;
using AuctionSystem.Interfaces;
using AuctionSystem.Vehicles.Segments;
using FluentAssertions;
using Moq;
using Xunit.Sdk;

namespace AuctionSystem.Tests;

public class AuctionInventoryTests
{
    private readonly AuctionInventory _auctionInventory = new();

    [Fact]
    public void AuctionSystem_AddVehiclesToInventory_MultipleAdds_HappyPath()
    {
        var hatch1 = new Hatchback("Skoda","Fabia", 2023, 10000, 5);
        var hatch2 = new Hatchback("Honda","Jazz", 2003, 1000, 5);
        var hatch3 = new Hatchback("Nissan","Almera", 2001, 500, 3);
        
        var sedan1 = new Sedan("Audi","A4", 2017, 7500, 5);
        var sedan2 = new Sedan("BMW","530d", 1993, 3500, 5);

        var suv1 = new SUV("BMW", "X1", 2015, 6000, 5);
        var suv2 = new SUV("Lancia", "Phedra", 2003, 1500, 7);
        
        var truck1 = new Truck("Ford","F150", 2020, 14000, 1500);
        var truck2 = new Truck("Ford","Ranger", 2005, 6000, 1750);
        var truck3 = new Truck("Toyota","Hylux", 2001, 2500, 2000);
        
        _auctionInventory.AddVehicles(hatch1, hatch2, hatch3,sedan1, sedan2);
        _auctionInventory.GetInventorySize().Should().Be(5);
        
        _auctionInventory.AddVehicle(suv1);
        _auctionInventory.GetInventorySize().Should().Be(6);
        
        _auctionInventory.AddVehicles(suv2, truck1, truck2, truck3);
        _auctionInventory.GetInventorySize().Should().Be(10);

        List<IVehicle> getVehiclesResult = _auctionInventory.GetVehicles(year: 2003);
        getVehiclesResult.Count.Should().Be(2);
    }

    [Theory]
    [InlineData("BMW", 2)]
    [InlineData("Ford", 2)]
    [InlineData("Toyota", 1)]
    [InlineData("Honda", 1)]
    [InlineData("Mercedes", 0)]
    public void AuctionInventory_GetVehiclesByManufacturer(string manufacturer, int expectedCount)
    {
        FillInventory();

        _auctionInventory.GetVehicles(manufacturer: manufacturer).Count.Should().Be(expectedCount);
    }
    
    [Theory]
    [InlineData(2001, 2)]
    [InlineData(2003, 2)]
    [InlineData(2023, 1)]
    [InlineData(2025, 0)]
    public void AuctionInventory_GetVehiclesByYear(int year, int expectedCount)
    {
        FillInventory();

        _auctionInventory.GetVehicles(year: year).Count.Should().Be(expectedCount);
    }
    
    [Theory]
    [InlineData("Sedan", 2)]
    [InlineData("Hatchback", 4)]
    [InlineData("SUV", 2)]
    [InlineData("Truck", 3)]
    public void AuctionInventory_GetVehiclesByType(string type, int expectedCount)
    {
        FillInventory();

        _auctionInventory.GetVehicles(type: type).Count.Should().Be(expectedCount);
    }
    
    [Theory]
    [InlineData("Fabia", 2)]
    [InlineData("X1", 1)]
    [InlineData("Civic", 0)]
    public void AuctionInventory_GetVehiclesByModel(string model, int expectedCount)
    {
        FillInventory();

        _auctionInventory.GetVehicles(model: model).Count.Should().Be(expectedCount);
    }

    [Fact]
    public void AuctionInventory_AddVehiclesToInventory_SentDuplicatedId_ThrowException()
    {
        var hatch1 = new Hatchback("Skoda","Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        Mock<IVehicle> mockVehicle = new();
        mockVehicle.Setup(x => x.Id).Returns(hatch1.Id);

        var exception = Assert.Throws<Exception>(() => _auctionInventory.AddVehicle(mockVehicle.Object));
        exception.Message.Should().Be("Vehicle already exists.");
    }

    [Fact]
    public void AuctionInventory_GetVehicleById_ShouldReturnVehicle()
    {
        var hatch1 = new Hatchback("Skoda","Fabia", 2023, 10000, 5);
        _auctionInventory.AddVehicle(hatch1);
        
        var hatch2 = _auctionInventory.GetVehicleById(hatch1.Id);
        hatch2?.Id.Should().Be(hatch1.Id);
    }
    
    [Fact]
    public void AuctionInventory_GetVehicleById_NoVehicleFound_ShouldReturnNull()
    {
        var hatch2 = _auctionInventory.GetVehicleById(Guid.Empty);
        hatch2.Should().BeNull();
    }

    private void FillInventory()
    {
        var hatch1 = new Hatchback("Skoda","Fabia", 2023, 10000, 5);
        var hatch2 = new Hatchback("Honda","Jazz", 2003, 1000, 5);
        var hatch3 = new Hatchback("Nissan","Almera", 2001, 500, 3);
        var hatch4 = new Hatchback("Skoda","Fabia", 2022, 6000, 5);
        
        var sedan1 = new Sedan("Audi","A4", 2017, 7500, 5);
        var sedan2 = new Sedan("BMW","530d", 1993, 3500, 5);

        var suv1 = new SUV("BMW", "X1", 2015, 6000, 5);
        var suv2 = new SUV("Lancia", "Phedra", 2003, 1500, 7);
        
        var truck1 = new Truck("Ford","F150", 2020, 14000, 1500);
        var truck2 = new Truck("Ford","Ranger", 2005, 6000, 1750);
        var truck3 = new Truck("Toyota","Hylux", 2001, 2500, 2000);

        _auctionInventory.AddVehicles(hatch1, hatch2, hatch3, hatch4, sedan1, sedan2, suv1, suv2, truck1, truck2,
            truck3);
    }
}