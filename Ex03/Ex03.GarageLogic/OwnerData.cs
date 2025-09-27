namespace Ex03.GarageLogic
{
    internal struct OwnerData
    {
        internal readonly string r_Name;
        internal string m_PhoneNumber;

        internal OwnerData(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_Name = i_OwnerName;
            m_PhoneNumber = i_OwnerPhoneNumber;
        }
    }
}
