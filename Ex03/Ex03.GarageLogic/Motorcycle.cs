using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public eLicenseType m_LicenseType;
        public int m_EngineCapacityInCC;

        public Motorcycle(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber)
            : base(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber)
        {
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            if (m_Engine is FuelEngine)
            {
                stringBuilder.AppendLine(string.Format("Vehicle Type: Petrol Motorcycle"));
            }
            else
            {
                stringBuilder.AppendLine(string.Format("Vehicle Type: Electric Motorcycle"));
            }

            stringBuilder.AppendLine(string.Format(@"License Type: {0}, Engine Volume: {1}", m_LicenseType.ToString(), m_EngineCapacityInCC));

            return stringBuilder.ToString();
        }

        public void SetProperties(Dictionary<string, string> i_VehicleExtraDetails)
        {
            if (i_VehicleExtraDetails.TryGetValue("licenseType", out string licenseTypeStr) && Enum.TryParse(licenseTypeStr, out Motorcycle.eLicenseType licenseType))
            {
                m_LicenseType = licenseType;
            }
            else
            {
                throw new KeyNotFoundException("LicenseType was not found in dictionary");
            }

            if (i_VehicleExtraDetails.TryGetValue("engineSize", out string engineSizeStr) && int.TryParse(engineSizeStr, out int engineSize))
            {
                m_EngineCapacityInCC = engineSize;
            }
            else
            {
                throw new KeyNotFoundException("engineSize was not found in dictionary");
            }

            if((int)m_VehicleType == 3)
            {
                if (i_VehicleExtraDetails.TryGetValue("hoursOfBatteryLeft", out string o_HoursOfBatteryLeftStr))
                {
                    m_Engine.SetEngineProperties(o_HoursOfBatteryLeftStr);
                }
                else
                {
                    throw new KeyNotFoundException("hoursOfBatteryLeft was not found in dictionary");
                }
            }
            else
            {
                if (i_VehicleExtraDetails.TryGetValue("litersOfFuelLeft", out string o_LitersOfFuelLeft))
                {
                    m_Engine.SetEngineProperties(o_LitersOfFuelLeft);
                }
                else
                {
                    throw new KeyNotFoundException("litersOfFuelLeft was not found in dictionary");
                }
            }

            calculateEnergyPercentage();
        }
    }
}
