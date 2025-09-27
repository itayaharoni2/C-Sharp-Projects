using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        GarageLogicManager m_GarageLogicManager = new GarageLogicManager();

        private readonly Dictionary<int, string> r_VehicleKey = new Dictionary<int, string>
        {
            {1, "car"},
            {2, "car"},
            {3, "motorcycle"},
            {4, "motorcycle"},
            {5, "truck"}
        };

        private readonly Dictionary<Vehicle.eVehicleStatus, string> r_StatusMap = new Dictionary<Vehicle.eVehicleStatus, string>
        {
            {Vehicle.eVehicleStatus.BeingFixed, "Being fixed"},
            {Vehicle.eVehicleStatus.Fixed, "Fixed"},
            {Vehicle.eVehicleStatus.Payed, "Payed"}
        };

        private enum eMainMenuOptions
        {
            addVehicle = 1,
            viewVehicleList = 2,
            changeVehicleStatus = 3,
            inflateTires = 4,
            addFuel = 5,
            chargeBattery = 6,
            viewVehicleStatus = 7,
            exit = 8
        }

        internal ConsoleUI()
        {
        }

        internal void LaunchGarageManagementSystem()
        {
            const bool v_InfiniteLoop = true;
            
            Console.WriteLine("Hello! Welcome to your garage management system.");
            while(v_InfiniteLoop)
            {
                Console.WriteLine("What do you want to do? Choose an action:");
                string userMenu = @"
                1. Add a new vehicle
                2. View all vehicles list
                3. Change vehicle status
                4. Inflate vehicle tires
                5. Add fuel to petrol tank
                6. Charge electric car
                7. View vehicle status
                8. Exit
                ";

                Console.WriteLine(userMenu);
                string menuChoiceStr = Console.ReadLine();
                while (!UserInputValidation.IsValidEnumInput(typeof(eMainMenuOptions), menuChoiceStr))
                {
                    Console.WriteLine("Please enter a valid option (i.e., a number between 1 and 8)");
                    menuChoiceStr = Console.ReadLine();
                }

                int.TryParse(menuChoiceStr, out int menuChoiceInt);
                switch ((eMainMenuOptions)menuChoiceInt)
                {
                    case eMainMenuOptions.addVehicle:
                        addNewVehicle();
                        break;
                    case eMainMenuOptions.viewVehicleList:
                        viewVehiclesList();
                        break;
                    case eMainMenuOptions.changeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eMainMenuOptions.inflateTires:
                        inflateTires();
                        break;
                    case eMainMenuOptions.addFuel:
                        addFuelToPetrolTank();
                        break;
                    case eMainMenuOptions.chargeBattery:
                        chargeElectricVehicle();
                        break;
                    case eMainMenuOptions.viewVehicleStatus:
                        viewVehicleStatus();
                        break;
                    case eMainMenuOptions.exit:
                        exitGarage();
                        break;
                }
            }
        }

        private void addNewVehicle()
        {
                Dictionary<string, string> newVehicleDetails = new Dictionary<string, string>();

                Console.WriteLine("Enter your vehicle's license plate number: ");
                string licensePlate = Console.ReadLine();
                if (m_GarageLogicManager.IsCarInGarage(licensePlate))
                {
                    if(m_GarageLogicManager.GetVehicleStatus(licensePlate) == Vehicle.eVehicleStatus.BeingFixed)
                    {
                        Console.WriteLine("This vehicle is already being fixed");
                        return;
                    }
                    else
                    {
                        m_GarageLogicManager.ChangeVehicleStatus(licensePlate, Vehicle.eVehicleStatus.BeingFixed);
                    }
                }
                else
                {
                    newVehicleDetails = getOwnerDetails();
                    newVehicleDetails.Add("licensePlate", licensePlate);
                    Console.WriteLine("What is the vehicle type? (Enter the correct menu number):\n1. Electric Car\n2. Petrol car\n3. Electric motorcycle\n4. Petrol motorcycle\n5. Truck");
                    newVehicleDetails.Add("vehicleType", Console.ReadLine());
                    while (!UserInputValidation.IsValidEnumInput(typeof(VehicleCreator.eVehicleType), newVehicleDetails["vehicleType"]))
                    {
                        Console.WriteLine("What is the vehicle type? (Enter the correct menu number):\n1. Electric Car\n2. Petrol car\n3. Electric motorcycle\n4. Petrol motorcycle\n5. Truck");
                        newVehicleDetails["vehicleType"] = Console.ReadLine();
                    }

                    m_GarageLogicManager.AddNewVehicle(newVehicleDetails);
                }

                sendExtraVehicleDetails(licensePlate);
        }

        private void sendExtraVehicleDetails(string io_licensePlate)
        {
            bool isDone = false;
            while (!isDone)
            {
                try
                {
                    Dictionary<string, string> existingVehicleDetails = new Dictionary<string, string>();
                    List<Dictionary<string, string>> tireDetails = new List<Dictionary<string, string>>();
                    VehicleCreator.eVehicleType vehicleType = m_GarageLogicManager.GetVehicleType(io_licensePlate);

                    existingVehicleDetails = getGeneralVehicleDetails(io_licensePlate, vehicleType);
                    existingVehicleDetails = getTypeSpecificVehicleDetails(existingVehicleDetails, vehicleType);
                    tireDetails = getTireDetails(vehicleType);
                    while (!UserInputValidation.IsVehicleDetailsValid(existingVehicleDetails, tireDetails, vehicleType))
                    {
                        Console.WriteLine("Please enter valid inputs");
                        existingVehicleDetails = getGeneralVehicleDetails(io_licensePlate, vehicleType);
                        existingVehicleDetails = getTypeSpecificVehicleDetails(existingVehicleDetails, vehicleType);
                        tireDetails = getTireDetails(vehicleType);
                    }

                    m_GarageLogicManager.StartNewJobOnVehicle(existingVehicleDetails, tireDetails);
                    Console.WriteLine("Your job has been added");
                    isDone = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void viewVehiclesList()
        {   
            Console.WriteLine("What list would you like to view?\n1.Being Fixed\n2. Fixed\n3. Payed\n4. All vehicles");
            string statusNumberStr = Console.ReadLine();
            while(!UserInputValidation.IsValidEnumInput(typeof(Vehicle.eVehicleStatus), statusNumberStr) || statusNumberStr.Equals("4"))
            {
                Console.WriteLine("Please choose a valid menu choice:\n1.Being Fixed\n2. Fixed\n3. Payed\n4. All vehicles");
                statusNumberStr = Console.ReadLine();
            }

            int.TryParse(statusNumberStr, out int statusNumber);
            Dictionary<string, Vehicle.eVehicleStatus> vehicleDictionary = m_GarageLogicManager.GetVehiclesListByStatus(statusNumber);
            foreach(var vehicle in vehicleDictionary)
            {
                Console.WriteLine(String.Format("License Number: {0} Status: {1}", vehicle.Key, r_StatusMap[vehicle.Value]));
            }
        }
        
        private void changeVehicleStatus()
        {
            try
            {
                Console.WriteLine("Please Enter the license plate of the vehicle:");
                string licensePlate = Console.ReadLine();
                Console.WriteLine("Please choose the state of the vehicle:\n1. Being Fixed\n2. Fixed\n3. Payed");
                string status = Console.ReadLine();
                while(!UserInputValidation.IsValidEnumInput(typeof(Vehicle.eVehicleStatus), status))
                {
                    Console.WriteLine("Please choose a valid state:\n1. Being Fixed\n2. Fixed\n3. Payed");
                    status = Console.ReadLine();
                }

                int.TryParse(status, out int statusInt);
                m_GarageLogicManager.ChangeVehicleStatus(licensePlate, (Vehicle.eVehicleStatus)statusInt);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("The status has been changed successfully");
        }

        private void inflateTires()
        {
            try
            {
                Console.WriteLine("Please Enter the license plate of the vehicle:");
                string licensePlate = Console.ReadLine();
                Console.WriteLine("Would you like to inflate tires to the max?\n1. Yes\n2. No");
                string choice = Console.ReadLine();
                while(!UserInputValidation.IsValidEnumInput(typeof(GarageLogicManager.eYesOrNo), choice))
                {
                    Console.WriteLine("Please enter a valid choice:\n1. Yes\n2. No");
                    choice = Console.ReadLine();
                }

                bool.TryParse(GarageLogicManager.ParseYesOrNo(choice), out bool choiceBool);
                if(choiceBool)
                {
                    m_GarageLogicManager.InflateTiresToMax(licensePlate);
                }
                else
                {
                    inflateTiresByAirPressure(licensePlate);
                }

                Console.WriteLine("Tires has been inflated");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void inflateTiresByAirPressure(string io_LicensePlate)
        {
            try
            {
                Console.WriteLine("Please enter the air pressure to add:");
                string airPressureToAddStr = Console.ReadLine();
                if (float.TryParse(airPressureToAddStr, out float airPressureToAdd) && airPressureToAdd >= 0)
                {
                    m_GarageLogicManager.InflateTires(io_LicensePlate, airPressureToAdd);
                }
                else
                {
                    Console.WriteLine("Invalid air pressure amount. Please enter a positive number.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void addFuelToPetrolTank()
        {
            try
            {
                Console.WriteLine("Please Enter the license plate of the vehicle:");
                string licensePlate = Console.ReadLine();
                Console.WriteLine("Choose the desired fuel type:\n1. Soler\n2. Octan95\n3. Octan96\n4. Octan98");
                string fuelType = Console.ReadLine();
                while(!UserInputValidation.IsValidEnumInput(typeof(FuelEngine.eFuelType), fuelType))
                {
                    Console.WriteLine("Please enter a valid option:\n1. Soler\n2. Octan95\n3. Octan96\n4. Octan98");
                    fuelType = Console.ReadLine();
                }

                Console.WriteLine("Enter the desired amount of fuel:");
                string fuelAmount = Console.ReadLine();
                while(!float.TryParse(fuelAmount, out _))
                {
                    Console.WriteLine("Please enter the number of liters to fuel: (Digits)");
                    fuelAmount = Console.ReadLine();
                }
                
                float.TryParse(fuelAmount, out float fuelAmountFloat);
                int.TryParse(fuelType, out int fuelTypeInt);
                m_GarageLogicManager.AddFuel(licensePlate, (FuelEngine.eFuelType)fuelTypeInt, fuelAmountFloat);
                Console.WriteLine("Vehicle has been fueled");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void chargeElectricVehicle()
        {
            try
            {
                Console.WriteLine("Please Enter the license plate of the vehicle:");
                string licensePlate = Console.ReadLine();
                Console.WriteLine("Enter the minutes of battery power to add:");
                string batteryAmount = Console.ReadLine();
                while(!float.TryParse(batteryAmount, out _))
                {
                    Console.WriteLine("Please enter the number of minutes of battery power to add: (Digits)");
                    batteryAmount = Console.ReadLine();
                }

                float.TryParse(batteryAmount, out float batteryAmountFloat);
                m_GarageLogicManager.ChargeElectricVehicle(licensePlate, batteryAmountFloat);
                Console.WriteLine("Battery has been charged");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void viewVehicleStatus()
        {
            try
            {
                Console.WriteLine("Please Enter the license plate of the vehicle:");
                string licensePlate = Console.ReadLine();
                Console.WriteLine(m_GarageLogicManager.GetVehicleObj(licensePlate).ToString());
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private Dictionary<string,string> getGeneralVehicleDetails(string io_LicensePlate, VehicleCreator.eVehicleType io_VehicleType)
        {   
            Dictionary<string,string> vehicleDetails = new Dictionary<string,string> {{"licensePlate", io_LicensePlate}};

            Console.WriteLine(string.Format("What is the model of the {0}?", r_VehicleKey[(int)io_VehicleType]));
            vehicleDetails.Add("model", Console.ReadLine());
            string vehicleTypeString = r_VehicleKey[(int)io_VehicleType];
            switch (io_VehicleType)
            {
                case VehicleCreator.eVehicleType.ElectricCar:
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                    Console.WriteLine(String.Format("How many hours of battery does the {0} have left?", vehicleTypeString));
                    vehicleDetails.Add("hoursOfBatteryLeft", Console.ReadLine());
                    break;
                case VehicleCreator.eVehicleType.PetrolCar:
                case VehicleCreator.eVehicleType.PetrolMotorcycle:
                case VehicleCreator.eVehicleType.Truck:
                    Console.WriteLine(String.Format("How many liters of fuel does the {0} have left?", vehicleTypeString));
                    vehicleDetails.Add("litersOfFuelLeft", Console.ReadLine());
                    break;
            }

            return vehicleDetails;
        }

        private static Dictionary<string, string> getTypeSpecificVehicleDetails(Dictionary<string, string> o_VehicleDetails, VehicleCreator.eVehicleType io_VehicleType)
        {
            switch (io_VehicleType)
            {
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                case VehicleCreator.eVehicleType.PetrolMotorcycle:
                    Console.WriteLine("What license does the motorcycle require? (Enter the correct menu number)\n1. A\n2. A1\n3. AA\n4. B1");
                    o_VehicleDetails.Add("licenseType", Console.ReadLine());
                    Console.WriteLine("What is the size of the motorcycle's engine?(Enter a number)");
                    o_VehicleDetails.Add("engineSize", Console.ReadLine());
                    break;
                case VehicleCreator.eVehicleType.ElectricCar:
                case VehicleCreator.eVehicleType.PetrolCar:
                    Console.WriteLine("What color is the Car? (Enter the correct menu number)\n1. Yellow\n2. White\n3. Red\n4. Black");
                    o_VehicleDetails.Add("carColor", Console.ReadLine());
                    Console.WriteLine("How many doors does your car have? (Enter the correct menu number)\n1. Two doors\n2. Three doors\n3. Four doors\n4. Five doors");
                    o_VehicleDetails.Add("numberOfDoors", Console.ReadLine());
                    break;
                case VehicleCreator.eVehicleType.Truck:
                    Console.WriteLine("Does the truck transfer dangerous materials? (Enter the correct menu number)\n1. Yes\n2. No");
                    o_VehicleDetails.Add("doesCarryDangerousMaterials", Console.ReadLine());
                    Console.WriteLine("What is the cargo capacity? (Enter a number)");
                    o_VehicleDetails.Add("cargoCapacity", Console.ReadLine());
                    break;
                }

                return o_VehicleDetails;
        }

        private static Dictionary<string, string> getOwnerDetails()
        {
            Dictionary<string, string> vehicleDetails = new Dictionary<string, string>();
            Console.WriteLine("Who is the owner of the vehicle?");
            vehicleDetails.Add("ownerName", Console.ReadLine());
            Console.WriteLine(String.Format("What is {0}'s phone number?", vehicleDetails["ownerName"]));
            vehicleDetails.Add("ownerPhoneNumber", Console.ReadLine());
            vehicleDetails.Add("vehicleStatus", "BeingFixed");

            return vehicleDetails;
        }

        private List<Dictionary<string, string>> getTireDetails(VehicleCreator.eVehicleType io_VehicleType)
        {
            List<Dictionary<string, string>> tireDetails = new List<Dictionary<string, string>>();
            int numOfWheels = m_GarageLogicManager.GetNumOfWheels(io_VehicleType);
            GarageLogicManager.eYesOrNo choice = identicalTireCheck();

            switch(choice)
            {
                case GarageLogicManager.eYesOrNo.Yes:
                    Dictionary<string, string> singleTireDetail = getSingleTireDetails();
                    for(int i = 0; i < numOfWheels; i++)
                    {
                        tireDetails.Add(singleTireDetail);
                    }
                    
                    break;
                case GarageLogicManager.eYesOrNo.No:
                    for(int i = 0; i < numOfWheels; i++)
                    {
                        tireDetails.Add(getSingleTireDetails());
                    }

                    break;
            }

            return tireDetails;
        }

        private Dictionary<string, string> getSingleTireDetails()
        {
            Dictionary<string, string> tireDetails = new Dictionary<string, string>();

            Console.WriteLine("Enter the tire manufacturer:");
            tireDetails.Add("manufacturer", Console.ReadLine());
            Console.WriteLine("Enter the current wheel pressure:");
            tireDetails.Add("currentAirPressure", Console.ReadLine());

            return tireDetails; 
        }

        private static GarageLogicManager.eYesOrNo identicalTireCheck()
        {
            Console.WriteLine("Are all the tires identical? (Enter the correct menu number)\n1. Yes\n2. No");
            string areTiresIdenticalStr = Console.ReadLine();
            while(!UserInputValidation.IsValidEnumInput(typeof(GarageLogicManager.eYesOrNo), areTiresIdenticalStr))
            {
                Console.WriteLine("Please enter a valid menu number\nAre all the tires identical? (Enter the correct menu number)\n1. Yes\n2. No");
                areTiresIdenticalStr = Console.ReadLine();
            }

            int.TryParse(areTiresIdenticalStr, out int areTiresIdenticalInt);

            return (GarageLogicManager.eYesOrNo)areTiresIdenticalInt;
        }

        private static void exitGarage()
        {
            Console.WriteLine("Goodbye...");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

    }
}