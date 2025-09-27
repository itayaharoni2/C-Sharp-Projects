using Ex03.GarageLogic.Ex03.GarageLogic;
using EX03.GarageLogic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : VehicleEngine
    {
        internal ElectricEngine(float i_MaxBatteryRunningHours)
            : base(i_MaxBatteryRunningHours)
        {
        }

        public override void AddEnergy(float i_BatteryHoursAmount)
        {
            if (i_BatteryHoursAmount < 0)
            {
                throw new ArgumentException("Battery charge amount cannot be negative.");
            }

            if (m_CurrentEnergyAmount + i_BatteryHoursAmount > r_MaxEnergyCapacity)
            {
                throw new ArgumentException("Cannot charge beyond maximum battery capacity.");
            }

            m_CurrentEnergyAmount += i_BatteryHoursAmount;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format(@"Max Battery Capacity: {0}, Remaining Battery Hours: {1}", r_MaxEnergyCapacity, m_CurrentEnergyAmount));
            
            return stringBuilder.ToString();
        }
        
        public override void SetEngineProperties(string i_HoursOfBatteryLeft)
        {
            if (float.TryParse(i_HoursOfBatteryLeft, out float o_HoursOfBatteryLeftFloat))
            {
                if (o_HoursOfBatteryLeftFloat <= r_MaxEnergyCapacity && 0 <= o_HoursOfBatteryLeftFloat)
                {
                    m_CurrentEnergyAmount = o_HoursOfBatteryLeftFloat;
                }
                else
                {
                    float minEnergyAmount = 0;
                    throw new ValueOutOfRangeException(o_HoursOfBatteryLeftFloat, minEnergyAmount, r_MaxEnergyCapacity);
                }
            }
            else
            {
                throw new FormatException("HoursOfBatteryLeft is not a float");
            }
        }
    }
}
