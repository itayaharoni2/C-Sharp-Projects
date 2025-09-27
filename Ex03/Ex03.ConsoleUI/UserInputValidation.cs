using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInputValidation
    {
        public static bool IsValidEnumInput(Type i_EnumType, string i_UserInput)
        {
            int userInputInt;

            return int.TryParse(i_UserInput, out userInputInt) && Enum.IsDefined(i_EnumType, userInputInt);
        }

        public static bool IsVehicleDetailsValid(Dictionary<string, string> io_VehicleDetails, List<Dictionary<string, string>> io_TireDetails, VehicleCreator.eVehicleType io_VehicleType)
        {
            bool isEnergyDetailsValid = true;
            bool isTypeDetailsValid = true;
            bool isTireDetailsValid = true;

            switch(io_VehicleType)
            {
                case VehicleCreator.eVehicleType.ElectricCar:
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                    isEnergyDetailsValid = float.TryParse(io_VehicleDetails["hoursOfBatteryLeft"], out _);
                    break;
                case VehicleCreator.eVehicleType.PetrolCar:
                case VehicleCreator.eVehicleType.PetrolMotorcycle:
                case VehicleCreator.eVehicleType.Truck:
                    isEnergyDetailsValid = float.TryParse(io_VehicleDetails["litersOfFuelLeft"], out _);
                    break;
            }

            switch (io_VehicleType)
            {
                case VehicleCreator.eVehicleType.ElectricMotorcycle:
                case VehicleCreator.eVehicleType.PetrolMotorcycle:
                    isTypeDetailsValid = IsValidEnumInput(typeof(Motorcycle.eLicenseType), io_VehicleDetails["licenseType"]) && int.TryParse(io_VehicleDetails["engineSize"], out _);
                    break;
                case VehicleCreator.eVehicleType.ElectricCar:
                case VehicleCreator.eVehicleType.PetrolCar:
                    isTypeDetailsValid = IsValidEnumInput(typeof(Car.eColor), io_VehicleDetails["carColor"]) && IsValidEnumInput(typeof(Car.eNumberOfDoors), io_VehicleDetails["numberOfDoors"]);
                    break;

                case VehicleCreator.eVehicleType.Truck:
                    isTypeDetailsValid = IsValidEnumInput(typeof(GarageLogicManager.eYesOrNo), io_VehicleDetails["doesCarryDangerousMaterials"]) && float.TryParse(io_VehicleDetails["cargoCapacity"], out _);
                    break;
            }

            foreach(var tire in io_TireDetails)
            {
                if(!float.TryParse(tire["currentAirPressure"], out _))
                {
                    isTireDetailsValid = false;
                    break;
                }
            }

            return isEnergyDetailsValid && isTypeDetailsValid && isTireDetailsValid;
        }
    }
}
