namespace Ex02_Memory_Game
{
    internal class PlayerData
    {
        internal bool m_IsPC { get; set; }
        internal string m_Name { get; set; }
        internal int m_Points { get; set; } = 0;

        internal PlayerData(bool i_IsPC, string i_Name)
        {
            this.m_IsPC = i_IsPC;
            this.m_Name = i_Name;
        }
    }
}