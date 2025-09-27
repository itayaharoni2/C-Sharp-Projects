using Ex03.GarageLogic.Ex03.GarageLogic;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public enum eVehicleType
        {
            ElectricCar = 1,
            PetrolCar = 2,
            ElectricMotorcycle = 3,
            PetrolMotorcycle = 4,
            Truck = 5
        }

        internal static readonly Dictionary<eVehicleType, int> sr_NumOfWheelsDict = new Dictionary<eVehicleType, int>()
        {
            { eVehicleType.ElectricCar, 5 },
            { eVehicleType.PetrolCar, 5 },
            { eVehicleType.ElectricMotorcycle, 2 },
            { eVehicleType.PetrolMotorcycle, 2 },
            { eVehicleType.Truck, 12 }
        };

        private static readonly Dictionary<eVehicleType, float> sr_MaxWheelAirPressureDict = new Dictionary<eVehicleType, float>()
        {
            { eVehicleType.ElectricCar, 31 },
            { eVehicleType.PetrolCar, 31 },
            { eVehicleType.ElectricMotorcycle, 33 },
            { eVehicleType.PetrolMotorcycle, 33 },
            { eVehicleType.Truck, 28 }
        };

        private static readonly Dictionary<eVehicleType, FuelEngine.eFuelType> sr_PetrolTypeDict = new Dictionary<eVehicleType, FuelEngine.eFuelType>()
        {
            { eVehicleType.PetrolCar, FuelEngine.eFuelType.Octan95 },
            { eVehicleType.PetrolMotorcycle, FuelEngine.eFuelType.Octan98 },
            { eVehicleType.Truck, FuelEngine.eFuelType.Soler }
        };

        private static readonly Dictionary<eVehicleType, float> sr_MaxEngineCapacityDict = new Dictionary<eVehicleType, float>()
        {
            { eVehicleType.ElectricCar, 3.5f },
            { eVehicleType.PetrolCar, 45f },
            { eVehicleType.ElectricMotorcycle, 2.5f },
            { eVehicleType.PetrolMotorcycle, 5.5f },
            { eVehicleType.Truck, 120f }
        };

        internal static Vehicle CreateNewVehicle(string i_LicensePlate, int i_VehicleType, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            eVehicleType vehicleType = (eVehicleType)i_VehicleType;
            Vehicle vehicle;
            VehicleEngine newEngine = createEngine(vehicleType);
            Tire[] newTires = createTires(vehicleType);

            switch (vehicleType)
            {
                case eVehicleType.ElectricCar:
                    vehicle = new Car(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber);  
                    break;
                case eVehicleType.PetrolCar:
                    vehicle = new Car(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber);
                    break;
                case eVehicleType.PetrolMotorcycle:
                    vehicle = new Motorcycle(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber);
                    break;
                default:
                    vehicle = null;
                    break;
            }

            vehicle.m_Engine = newEngine;
            vehicle.m_Tires = newTires;
            vehicle.m_VehicleType = vehicleType;

            return vehicle;
        }

        internal static string[] GetVehicleTypes()
        {
            return Enum.GetNames(typeof(eVehicleType));
        }

        private static VehicleEngine createEngine(eVehicleType i_VehicleType)
        {
            VehicleEngine newEngine = null;

            if (i_VehicleType == eVehicleType.PetrolCar || i_VehicleType == eVehicleType.PetrolMotorcycle || i_VehicleType == eVehicleType.Truck)
            {
                newEngine = new FuelEngine(sr_MaxEngineCapacityDict[i_VehicleType], sr_PetrolTypeDict[i_VehicleType]);
            }
            else if (i_VehicleType == eVehicleType.ElectricCar || i_VehicleType == eVehicleType.ElectricMotorcycle)
            {
                newEngine = new ElectricEngine(sr_MaxEngineCapacityDict[i_VehicleType]);
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type", nameof(i_VehicleType));
            }

            return newEngine;
        }

        private static Tire[] createTires(eVehicleType i_VehicleType)
        {
            Tire[] newTires = new Tire[sr_NumOfWheelsDict[i_VehicleType]];

            for (int i = 0; i < newTires.Length; i++)
            {
                newTires[i] = new Tire(sr_MaxWheelAirPressureDict[i_VehicleType]);
            }

            return newTires;
        }
    }
}
