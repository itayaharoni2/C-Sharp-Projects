namespace Ex02_Memory_Game
{
    internal class BoardCell<T>
    {
        internal T m_CellValue { get; set; }
        internal bool m_IsRevealed { get; set; } = false;
        internal bool m_WasRevealed { get; set; } = false;

        internal BoardCell(T i_cellValue)
        {
            this.m_CellValue = i_cellValue;
        }

        internal void RevealCell()
        {
            m_IsRevealed = true;
            m_WasRevealed = true;
        }

        internal void HideCell()
        {
            m_IsRevealed = false;
        }
    }
}

