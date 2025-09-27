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

## 🧩 Class Diagram
![Class Diagram](<./Class Diagram.png>)
