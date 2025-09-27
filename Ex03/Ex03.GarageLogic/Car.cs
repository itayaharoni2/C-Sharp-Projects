using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Yellow = 1,
            White = 2,
            Red = 3,
            Black = 4
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three = 2,
            Four = 3,
            Five = 4
        }
        public eColor m_Color { get; set; }
        public eNumberOfDoors m_NumberOfDoors { get; set; }

        public Car(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber)
            : base(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber)
        {
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            if (m_Engine is FuelEngine)
            {
                stringBuilder.AppendLine(string.Format("Vehicle Type: Petrol Car"));
            }
            else
            {
                stringBuilder.AppendLine(string.Format("Vehicle Type: Electric Car"));
            }

            stringBuilder.AppendLine(string.Format(@"Number of doors: {0},  Color: {1}", m_NumberOfDoors.ToString(), m_Color.ToString()));

            return stringBuilder.ToString();
        }

        public void SetProperties(Dictionary<string, string> i_VehicleExtraDetails)
        {
            if (i_VehicleExtraDetails.TryGetValue("numberOfDoors", out string numberOfDoorsStr) && Enum.TryParse(numberOfDoorsStr, true, out Car.eNumberOfDoors numberOfDoors))
            {
                m_NumberOfDoors = numberOfDoors;
            }
            else
            {
                throw new KeyNotFoundException("numberOfDoors was not found in dictionary");
            }

            if (i_VehicleExtraDetails.TryGetValue("carColor", out string carColorStr) && Enum.TryParse(carColorStr, out Car.eColor carColor))
            {
                m_Color = carColor;
            }
            else
            {
                throw new KeyNotFoundException("engineSize was not found in dictionary");
            }
            if ((int)m_VehicleType == 1)
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