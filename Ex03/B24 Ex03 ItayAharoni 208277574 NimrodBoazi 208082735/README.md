# Garage Manager (C#/.NET)

A console-based CRM-style app to manage a **Garage Management System**: register vehicles (cars, motorcycles, trucks), track repair status, and apply OOP/SOLID design throughout.

## ✨ Features
- Register vehicles with typed metadata (car/motorcycle/truck)
- Track repair state (e.g., admitted → in service → ready)
- Polymorphic operations per vehicle type
- Encapsulated business logic with interfaces and patterns
- Clear separation between UI layer and core logic

## 🧱 Tech
- **Language:** C# (target: .NET 8 or .NET 6)
- **Paradigms:** OOP, SOLID, Design Patterns
- **CLI:** dotnet SDK

## ▶️ Run Locally
```bash
# From the solution folder:
dotnet --info         # verify SDK is installed
dotnet restore        # restore dependencies (if any)
dotnet build -c Release
dotnet run --project <PathToProject>   # or run from the project folder: dotnet run
```

> If the solution contains multiple projects, open the `.sln` in Visual Studio / Rider and set the console app as **Startup Project**, then **Run**.

## 🧪 Example Flow
- Add a vehicle → choose type → enter license + specs
- Change status (admitted/in-service/ready)
- Query vehicles by type/status/plate
- Save/Load (if implemented in your version)

## 📁 Typical Structure
```
/src
  GarageManager.sln
  GarageManager.Core/          # domain & business logic
  GarageManager.Cli/           # console UI (Program.cs)
  GarageManager.Tests/         # unit tests (if present)
```

## 🔧 Troubleshooting
- **SDK not found:** install the .NET SDK from https://dotnet.microsoft.com/download
- **Multiple entry points:** ensure only the CLI project is set as startup or pass `--project` to `dotnet run`
- **Encoding/console issues:** run in a standard terminal (cmd/PowerShell) or switch code page to UTF-8

## 📜 License
MIT (or your preference).

---

## ⬆️ How to Upload to GitHub

### Option A — New repository from scratch
```bash
# in the project root (folder with the .sln)
git init
git add .
git commit -m "Initial commit: Garage Manager"
git branch -M main
git remote add origin https://github.com/<your-username>/garage-manager.git
git push -u origin main
```

### Option B — Existing repo (add this project inside)
```bash
git checkout -b garage-manager
git add .
git commit -m "Add C# Garage Manager project"
git push -u origin garage-manager
# then open a Pull Request on GitHub
```

### Option C — GitHub website
1. Create a repo on GitHub → **Add file → Upload files**.
2. Drag your folders/files (including `.sln` and project directories).
3. Add this `README.md` and **Commit**.
