using EX03.GarageLogic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Tire
    {
        internal readonly float r_MaxAirPressure;
        internal string? m_Manufacturer { get; set; }
        internal float? m_CurrentAirPressure { get; set; } = null;

        internal Tire(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void Inflate(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd < 0)
            {
                throw new ArgumentException("Amount cannot be negative.", nameof(i_AirPressureToAdd));
            }

            float currentAirPressure = m_CurrentAirPressure ?? 0;
            float newAirPressure = currentAirPressure + i_AirPressureToAdd;
            if (newAirPressure > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(newAirPressure, 0, r_MaxAirPressure);
            }

            m_CurrentAirPressure = newAirPressure;
        }

        public void InflateToMax()
        {
            float currentAirPressure = m_CurrentAirPressure ?? 0;
            float airPressureToAdd = r_MaxAirPressure - currentAirPressure;
            Inflate(airPressureToAdd);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format(@"Tire Manufacturer: {0}, Tire Air Pressure: {1}, Tire Max Air Pressure: {2}", m_Manufacturer, m_CurrentAirPressure, r_MaxAirPressure));
            return stringBuilder.ToString();
        }
    }
}
