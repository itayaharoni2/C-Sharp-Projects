using System;

namespace Ex02_Memory_Game
{
    internal class GameLogicManager<T> where T : struct
    {
        internal enum eCurrentPlayer
        {
            Player1 = 0,
            Player2 = 1
        }

        internal GameBoard<T> m_Board { get; set; }
        private PlayerData[] players = new PlayerData[2];
        private PcPlayerLogic<T> pcPlayerLogic;
        private eCurrentPlayer currentPlayer = eCurrentPlayer.Player1;

        internal GameLogicManager(string io_Player1Name, string io_Player2Name, bool io_IsPlayer2PC)
        {
            players[(int)eCurrentPlayer.Player1] = new PlayerData(false, io_Player1Name);
            if (io_IsPlayer2PC)
            {
                players[(int)eCurrentPlayer.Player2] = new PlayerData(true, io_Player2Name);
                pcPlayerLogic = new PcPlayerLogic<T>(players[(int)eCurrentPlayer.Player2]);
            }
            else
            {
                players[(int)eCurrentPlayer.Player2] = new PlayerData(false, io_Player2Name);
            }
        }

        internal void InitBoard(int o_NumOfRows, int o_NumOfColumns, Func<int, T> o_ValueGenerator)
        {
            m_Board = new GameBoard<T>(o_NumOfColumns, o_NumOfRows, o_ValueGenerator);
        }

        internal static bool IsDimensionsValid(int i_NumOfRows, int i_NumOfColumns)
        {
            return i_NumOfRows >= 4 && i_NumOfColumns >= 4 && i_NumOfRows <= 6 && i_NumOfColumns <= 6 && (i_NumOfRows * i_NumOfColumns) % 2 == 0;
        }

        internal bool IsCellChoiceValid(string i_CellChoice)
        {
            bool isValid = true;

            if (i_CellChoice.Length == 2)
            {
                char chosenRow = i_CellChoice[1];
                char chosenColumn = i_CellChoice[0];

                isValid = chosenColumn >= 'A' && chosenColumn < 'A' + m_Board.m_NumOfColumns &&
                          chosenRow >= '1' && chosenRow < '1' + m_Board.m_NumOfRows && (!m_Board.IsCellRevealed(i_CellChoice));
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }


        internal void RevealCell(string io_Cell)
        {
            m_Board.RevealCell(io_Cell);
        }

        internal void HideCell(string io_Cell)
        {
            m_Board.HideCell(io_Cell);
        }

        internal bool IsMatchingCells(string io_Cell1, string io_Cell2)
        {
            return m_Board.IsMatchingCells(io_Cell1, io_Cell2);
        }

        internal void AddPoint()
        {
            players[(int)currentPlayer].m_Points++;
        }

        internal void SwitchCurrentPlayer()
        {
            currentPlayer = (eCurrentPlayer)(((int)currentPlayer + 1) % 2);
        }

        internal bool CheckIfCurrentPlayerIsPC()
        {
            return players[(int)currentPlayer].m_IsPC;
        }

        internal string[] GetPcCellSelection()
        {
            return pcPlayerLogic.GetPcCellSelection(m_Board);
        }

        internal int DecideWinner()
        {
            int winner = 1;

            if (players[0].m_Points > players[1].m_Points)
            {
                winner = 0;
            }

            return winner;
        }

        internal bool IsBoardFullyRevealed()
        {
            return m_Board.IsBoardFullyRevealed();
        }

        internal string GetCurrentPlayerName()
        {
            return players[(int)currentPlayer].m_Name;
        }
    }
}