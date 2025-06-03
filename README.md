# Auction 
The goal for this small project was to implement a simple car auction management system.

There were multiple requirements, but we can split them between **Logic Classes** and **Object Classes**

## Object classes
The main object for this project is the Vehicle that will be auctioned off. Currently there are 4 types of vehicles:
* Hatchback
* Sedan
* SUV
* Truck

Since all 4 types have matching properties, the approach was to create an abstract class called AuctionableVehicle from which all the other Vehicle types were derived from.
That way, we are able to differentiate between different vehicle types, but at the same types, we could create methods that applied to all of them.

Another aspect to keep in mind is that AuctionableVehicle implements two interfaces: **IVehicle** and **IAuctionable**. These two interfaces were created to future proof the solution. All the auction behaviour comes from IAuctioanble and all the vehicle behaviour from IVehicle. In the future case we may need vehicles that are not supposed to be auctioned off we already have the behaviour split into different interfaces. 
If we knew from the start that auctioning will be a core feature and there are no vehicles that cannot be auctioned, then IVehicle and IAuctionable can be combined into a single interface.


## Logic Classes
For our Auction System, we need an Inventory for all the available cars to be auctioned. Therefore, **AuctionInventory** was created.

This class is responsible for managing the Inventory by adding new cars and by getting them based on its properties.

And finally, the **AuctionService** will manage the auctions themselves. Its responsible by creating and closing the auction as well as placing the bids.
