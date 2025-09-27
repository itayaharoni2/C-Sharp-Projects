# Garage Manager (C#/.NET)

A console-based CRM-style app to manage a **Garage Management System**: register vehicles (cars, motorcycles, trucks), track repair status, and apply OOP/SOLID design throughout.

## Features
- Register vehicles with typed metadata (car/motorcycle/truck)
- Track repair state (e.g., admitted → in service → ready)
- Polymorphic operations per vehicle type
- Encapsulated business logic with interfaces and patterns
- Clear separation between UI layer and core logic

## Tech
- **Language:** C# (target: .NET 8 or .NET 6)
- **Paradigms:** OOP, SOLID, Design Patterns
- **CLI:** dotnet SDK

## Run Locally
```bash
# From the solution folder:
dotnet --info         # verify SDK is installed
dotnet restore        # restore dependencies
dotnet build -c Release
dotnet run --project <PathToProject>
```

---

## Class / Enums List:

**ConsoleUI**
ConsoleUi: Handles the interface with the console user
Program: Where the main is located, this is where the program starts
UserInputValidation: checks if the user input is techniclly good, without knowing the logic

**GarageLogic**
Vehicle: holds the properties that every vehicle has, and few general methods
Car: inherit from vehicle, holds the car properties
Motorcycle: inherit from Vehicle, holds the motorcycle properties
Truck: inherit from Vehicle, holds the truck properties
OwnerData: holds the data of every car owner that put his car to the garage
Tire: handles all the tire methods, every vehicle has a set of tires
VehicleEngine: abstract class, holds general engine members and decides what methods every engine should implement according to his own conditions
FuelEngine: inherit from VehicleEngine, handles the methods related to fuel and engine in vehicles that are powered by petrol
ElectricEngine: inherit from VehicleEngine, implements the methods in a way thats relevant to electric vehicles
VehicleCreator: "creates" new vehicles when they are coming to the garage for the first time. Built in a way that adding a new type of vehicle is hanled easily
ValueOutOfRangeException: handles the exceptions that are related to a given value (such as fuel to add or air pressure to add) cant be added logiclly.
GarageLogicManager: running the garage. All the methods that ConsoleUI is holding for the user are handled by calling this class

---

## 🧩 Class Diagram
![Class Diagram](<./Class Diagram.png>)
