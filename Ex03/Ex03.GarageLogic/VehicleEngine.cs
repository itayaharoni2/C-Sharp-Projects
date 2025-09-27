using System.Text;

namespace Ex03.GarageLogic
{
    namespace Ex03.GarageLogic
    {
        public abstract class VehicleEngine
        {
            public float r_MaxEnergyCapacity { get; private set; }
            public float m_CurrentEnergyAmount { get; set; } = 0;

            public VehicleEngine(float i_MaxEnergyCapacity)
            {
                r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            }

            public abstract void AddEnergy(float i_Amount);
            public abstract void SetEngineProperties(string i_EnergyAmount);

            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.AppendLine(string.Format(@"Current Energy Amount: {0}", m_CurrentEnergyAmount));

                return stringBuilder.ToString();
            }
        }
    }

}
