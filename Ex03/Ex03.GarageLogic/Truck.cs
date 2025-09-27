using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        internal bool m_IsCarryingDangerousMaterials {  get; set; }
        internal float m_CargoCapacity {  get; set; }

        public Truck(string i_LicensePlate, string i_OwnerName, string i_OwnerPhoneNumber)
            : base(i_LicensePlate, i_OwnerName, i_OwnerPhoneNumber)
        {
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine(string.Format(@"Does Truck Carry dangerous materials: {0},
                                                     Cargo Capacity: {1}", m_IsCarryingDangerousMaterials, m_CargoCapacity));

            return stringBuilder.ToString();
        }

        public void SetProperties(Dictionary<string, string> i_VehicleExtraDetails)
        {
            if (i_VehicleExtraDetails.TryGetValue("cargoCapacity", out string cargoCapacityStr) && float.TryParse(cargoCapacityStr, out float cargoCapacity))
            {
                m_CargoCapacity = cargoCapacity;
            }
            else
            {
                throw new KeyNotFoundException("cargoCapacity was not found in dictionary");
            }

            if (i_VehicleExtraDetails.TryGetValue("doesCarryDangerousMaterials", out string doesCarryDangerousMaterialsStr) && bool.TryParse(GarageLogicManager.ParseYesOrNo(doesCarryDangerousMaterialsStr), out bool doesCarryDangerousMaterials))
            {
                m_IsCarryingDangerousMaterials = doesCarryDangerousMaterials;
            }
            else
            {
                throw new KeyNotFoundException("doesCarryDangerousMaterials was not found in dictionary");
            }

            if (i_VehicleExtraDetails.TryGetValue("litersOfFuelLeft", out string o_LitersOfFuelLeft))
            {
                m_Engine.SetEngineProperties(o_LitersOfFuelLeft);
            }
            else
            {
                throw new KeyNotFoundException("litersOfFuelLeft was not found in dictionary");
            }

            calculateEnergyPercentage();
        }
    }
}
