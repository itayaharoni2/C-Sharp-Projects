using Ex03.GarageLogic.Ex03.GarageLogic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        public enum eVehicleStatus
        {
            BeingFixed = 1,
            Fixed = 2,
            Payed = 3
        }

        internal readonly string r_LicensePlate;
        internal VehicleCreator.eVehicleType m_VehicleType { get; set; }
        internal string m_Model { get; set; }
        internal float m_energyPercentage { get; set; } = 0;
        internal Tire[] m_Tires;
        internal OwnerData m_OwnerData { get; set; }
        internal eVehicleStatus m_VehicleStatus { get; set; }
        internal VehicleEngine m_Engine { get; set; }

        internal Vehicle(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_LicensePlate = i_LicensePlate;
            m_OwnerData = new OwnerData(i_OwnerName, i_OwnerPhoneNumber);
            m_VehicleStatus = eVehicleStatus.BeingFixed;
        }

        internal void FillEngine(float i_AmountToAdd)
        {
            m_Engine.AddEnergy(i_AmountToAdd);
            calculateEnergyPercentage();
        }

        internal void calculateEnergyPercentage()
        {
            if (m_Engine.m_CurrentEnergyAmount == 0)
            {
                m_energyPercentage = 0;
            }
            else
            {
                m_energyPercentage = (m_Engine.m_CurrentEnergyAmount / m_Engine.r_MaxEnergyCapacity) * 100;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("License Plate: {0}", r_LicensePlate));
            stringBuilder.AppendLine(string.Format("Owner Name: {0}", m_OwnerData.r_Name));
            stringBuilder.AppendLine(string.Format("Owner Phone Number: {0}", m_OwnerData.m_PhoneNumber));
            stringBuilder.AppendLine(string.Format("Vehicle status: {0}", m_VehicleStatus.ToString()));
            stringBuilder.AppendLine(string.Format("Model: {0}", m_Model));
            stringBuilder.AppendLine(string.Format("Vehicle energy percentage: {0}", m_energyPercentage));
            stringBuilder.AppendLine(m_Engine.ToString());
            int tireCount = 1;
            foreach (Tire tire in m_Tires)
            {
                stringBuilder.AppendLine(string.Format("Tire {0}: {1}", tireCount, tire.ToString()));
                tireCount++;
            }

            return stringBuilder.ToString();
        }
    }
}
