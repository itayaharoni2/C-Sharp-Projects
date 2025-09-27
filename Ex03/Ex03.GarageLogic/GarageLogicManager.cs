using EX03.GarageLogic;
using static Ex03.GarageLogic.Vehicle;
using static Ex03.GarageLogic.VehicleCreator;

namespace Ex03.GarageLogic
{
    public class GarageLogicManager
    {
        private Dictionary<string, Vehicle> m_GarageVehicles;

        public GarageLogicManager()
        {
            m_GarageVehicles = new Dictionary<string, Vehicle>();
        }

        public void AddNewVehicle(Dictionary<string, string> io_NewVehicleDetails)
        {
            try
            {
                string newLicensePlate = io_NewVehicleDetails["licensePlate"];
                string vehicleTypeString = io_NewVehicleDetails["vehicleType"];
                string ownerName = io_NewVehicleDetails["ownerName"];
                string ownerPhoneNumber = io_NewVehicleDetails["ownerPhoneNumber"];
                int newVehicleType = int.Parse(vehicleTypeString);
                Vehicle vehicle = VehicleCreator.CreateNewVehicle(newLicensePlate, newVehicleType, ownerName, ownerPhoneNumber);

                m_GarageVehicles.Add(newLicensePlate, vehicle);
            }
            catch (KeyNotFoundException exception)
            {
                Console.WriteLine($"Key not found: {exception.Message}");
            }
            catch (FormatException exception)
            {
                Console.WriteLine($"Invalid format: {exception.Message}");
            }
        }

        public void StartNewJobOnVehicle(Dictionary<string, string> io_VehicleExtraDetails, List<Dictionary<string, string>> io_TireDetails)
        {
            string licensePlate = io_VehicleExtraDetails["licensePlate"];
            Vehicle vehicle = GetVehicleObj(licensePlate);

            if (io_VehicleExtraDetails.TryGetValue("model", out string model))
            {
                vehicle.m_Model = model;
            }

            initTires(io_TireDetails, vehicle);
            VehicleCreator.eVehicleType vehicleType = GetVehicleType(licensePlate);
            switch (vehicleType)
            {
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                case VehicleCreator.eVehicleType.PetrolMotorcycle:
                    if (vehicle is Motorcycle motorcycle)
                    {
                        motorcycle.SetProperties(io_VehicleExtraDetails);
                    }
                    break;
                case VehicleCreator.eVehicleType.ElectricCar:
                case VehicleCreator.eVehicleType.PetrolCar:
                    if (vehicle is Car car)
                    {
                        car.SetProperties(io_VehicleExtraDetails);
                    }
                    break;
                case VehicleCreator.eVehicleType.Truck:
                    if (vehicle is Truck truck)
                    {
                        truck.SetProperties(io_VehicleExtraDetails);
                    }
                    break;
            }  
        }

        private static void initTires(List<Dictionary<string, string>> io_TireDetails, Vehicle io_Vehicle)
        {
            if (io_TireDetails.Count != io_Vehicle.m_Tires.Length)
            {
                throw new ArgumentException("The number of tires provided does not match the number of tires on the vehicle.");
            }

            for (int i = 0; i < io_TireDetails.Count; i++)
            {
                var tireDetail = io_TireDetails[i];
                Tire tire = io_Vehicle.m_Tires[i];
                if (tireDetail.TryGetValue("manufacturer", out string manufacturer))
                {
                    tire.m_Manufacturer = manufacturer;
                }

                if (tireDetail.TryGetValue("currentAirPressure", out string currentAirPressureStr) && float.TryParse(currentAirPressureStr, out float currentAirPressure))
                {
                    if (currentAirPressure <= tire.r_MaxAirPressure && 0 <= currentAirPressure)
                    {
                        tire.m_CurrentAirPressure = currentAirPressure;
                    }
                    else
                    {
                        float minPressure = 0;
                        throw new ValueOutOfRangeException(currentAirPressure, minPressure, tire.r_MaxAirPressure);
                    }
                }
            }
        }

        public bool IsCarInGarage(string i_LicensePlate)
        {
            return m_GarageVehicles.ContainsKey(i_LicensePlate);
        }

        public void ChangeVehicleStatus(string i_LicensePlate, Vehicle.eVehicleStatus i_NewStatus)
        {
            Vehicle vehicle = GetVehicleObj(i_LicensePlate);

            if (vehicle.m_VehicleStatus == i_NewStatus)
            {
                throw new ArgumentException($"The vehicle with license plate {vehicle.r_LicensePlate} is already in the status {i_NewStatus}.");
            }
            else
            {
                vehicle.m_VehicleStatus = i_NewStatus;
            }
        }

        public Vehicle GetVehicleObj(string i_LicensePlate)
        {
            if (m_GarageVehicles.TryGetValue(i_LicensePlate, out Vehicle vehicle))
            {
                return vehicle;
            }
            else
            {
                throw new KeyNotFoundException("License plate not found.");
            }
        }

        public Vehicle.eVehicleStatus GetVehicleStatus(string i_LicensePlate)
        {
            return GetVehicleObj(i_LicensePlate).m_VehicleStatus;
        }

        public VehicleCreator.eVehicleType GetVehicleType(string i_LicensePlate)
        {
            if (m_GarageVehicles.TryGetValue(i_LicensePlate, out Vehicle vehicle))
            {
                return vehicle.m_VehicleType;
            }
            else
            {
                throw new KeyNotFoundException("License plate not found.");
            }
        }

        public Dictionary<string, eVehicleStatus> GetVehiclesListByStatus(int i_StatusInt)
        {
            if (!Enum.IsDefined(typeof(eVehicleStatus), i_StatusInt))
            {
                return m_GarageVehicles.ToDictionary(kv => kv.Key, kv => kv.Value.m_VehicleStatus);
            }

            eVehicleStatus status = (eVehicleStatus)i_StatusInt;

            return m_GarageVehicles
                .Where(kv => kv.Value.m_VehicleStatus == status)
                .ToDictionary(kv => kv.Key, kv => kv.Value.m_VehicleStatus);
        }

        public void InflateTiresToMax(string i_LicensePlate)
        {
            if (m_GarageVehicles.TryGetValue(i_LicensePlate, out Vehicle vehicle))
            {
                foreach (var tire in vehicle.m_Tires)
                {
                    tire.InflateToMax();
                }
            }
            else
            {
                throw new KeyNotFoundException($"Vehicle with license plate {i_LicensePlate} not found in the garage.");
            }
        }

        public void InflateTires(string i_LicensePlate, float i_AirPressureToAdd)
        {
            if (m_GarageVehicles.TryGetValue(i_LicensePlate, out Vehicle vehicle))
            {
                foreach (var tire in vehicle.m_Tires)
                {
                    tire.Inflate(i_AirPressureToAdd);
                }
            }
            else
            {
                throw new KeyNotFoundException($"Vehicle with license plate {i_LicensePlate} not found in the garage.");
            }
        }

        public void ChargeElectricVehicle(string i_LicensePlate, float i_ChargeMinutes)
        {
            if (i_ChargeMinutes < 0)
            {
                throw new ArgumentException("Charge amount cannot be negative.");
            }

            Vehicle vehicle = GetVehicleObj(i_LicensePlate);

            if (vehicle.m_Engine is ElectricEngine electricEngine)
            {
                float chargeHours = i_ChargeMinutes / 60.0f;
                electricEngine.AddEnergy(chargeHours);
            }
            else
            {
                throw new ArgumentException("Vehicle does not have an electric engine.");
            }
        }

        public void AddFuel(string i_LicensePlate, FuelEngine.eFuelType i_FuelType, float i_FuelAmount)
        {
            if (i_FuelAmount < 0)
            {
                throw new ArgumentException("Fuel amount cannot be negative.");
            }

            Vehicle vehicle = GetVehicleObj(i_LicensePlate);
            if (vehicle.m_Engine is FuelEngine fuelEngine)
            {
                fuelEngine.AddEnergy(i_FuelAmount, i_FuelType);
            }
            else
            {
                throw new ArgumentException("Vehicle does not have a fuel engine.");
            }
        }

        public enum eYesOrNo
        {
            Yes = 1,
            No = 2
        }

        public static string ParseYesOrNo(string i_Value)
        {
            string parsedValue;

            int.TryParse(i_Value, out int valueInt);
            if((eYesOrNo)valueInt == eYesOrNo.Yes)
            {
                parsedValue = "true";
            }
            else
            {
                parsedValue = "false";
            }

            return parsedValue;
        }

        public int GetNumOfWheels(eVehicleType io_VehicleType)
        {
            if (VehicleCreator.sr_NumOfWheelsDict.TryGetValue(io_VehicleType, out int numOfWheels))
            {
                return numOfWheels;
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type", nameof(io_VehicleType));
            }
        }

        public void GetVehicleEnergyPercentage(string i_LicensePlate, Dictionary<string, string> i_Properties)
        {
            Vehicle vehicle = GetVehicleObj(i_LicensePlate);
            if (vehicle == null)
            {
                throw new KeyNotFoundException($"Vehicle with license plate {i_LicensePlate} not found in the garage.");
            }

            try
            {
                vehicle.calculateEnergyPercentage();
                Console.WriteLine($"Energy percentage for vehicle {i_LicensePlate} updated to {vehicle.m_energyPercentage}%.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update energy percentage: " + ex.Message, ex);
            }
        }
    }
}
