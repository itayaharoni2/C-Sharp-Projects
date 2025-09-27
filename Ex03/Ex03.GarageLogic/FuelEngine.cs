using Ex03.GarageLogic.Ex03.GarageLogic;
using EX03.GarageLogic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : VehicleEngine
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }

        public eFuelType m_FuelType { get; set; }

        public FuelEngine(float i_MaxFuelAmount, eFuelType i_FuelType)
            : base(i_MaxFuelAmount)
        {
            m_FuelType = i_FuelType;
        }

        public override void AddEnergy(float i_AmountToAdd)
        {
            if (i_AmountToAdd < 0)
            {
                throw new ArgumentException("Fuel amount cannot be negative.");
            }

            if (m_CurrentEnergyAmount + i_AmountToAdd > r_MaxEnergyCapacity)
            {
                throw new ArgumentException("Cannot add fuel beyond maximum fuel capacity.");
            }

            m_CurrentEnergyAmount += i_AmountToAdd;
        }

        public void AddEnergy(float io_AmountToAdd, eFuelType i_FuelType)
        {
            if(i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Fuel type does not match vehicle");
            }

            AddEnergy(io_AmountToAdd); 
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format(@"Max Fuel Capacity: {0}, Fuel Type: {1}", r_MaxEnergyCapacity, m_FuelType));
            
            return stringBuilder.ToString();
        }

        public override void SetEngineProperties(string i_LitersOfFuelLeft)
        {
            if (float.TryParse(i_LitersOfFuelLeft, out float o_LitersOfFuelLeftFloat))
            {
                if (o_LitersOfFuelLeftFloat <= r_MaxEnergyCapacity && 0 <= o_LitersOfFuelLeftFloat)
                {
                    m_CurrentEnergyAmount = o_LitersOfFuelLeftFloat;
                }
                else
                {
                    float minFuelAmount = 0;
                    throw new ValueOutOfRangeException(o_LitersOfFuelLeftFloat, minFuelAmount, r_MaxEnergyCapacity);
                }
            }
            else
            {
                throw new FormatException("LitersOfFuelLeft is not a float");
            }
        }
    }
}
