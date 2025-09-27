using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02_Memory_Game
{
    internal class GameBoard<T>
    {
        internal BoardCell<T>[,] m_CellArray { get; private set; }
        internal int m_NumOfRows { get; private set; }
        internal int m_NumOfColumns { get; private set; }

        internal GameBoard(int i_NumOfRows, int i_NumOfColumns, Func<int, T> i_ValueGenerator)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfColumns = i_NumOfColumns;
            m_CellArray = new BoardCell<T>[i_NumOfRows, i_NumOfColumns];
            List<Tuple<int, int>> cellPositions = new List<Tuple<int, int>>();

            for (int row = 0; row < i_NumOfRows; row++)
            {
                for (int col = 0; col < i_NumOfColumns; col++)
                {
                    cellPositions.Add(Tuple.Create(row, col));
                }
            }

            Random randomLocation = new Random();
            int valueIndex = 0;

            while (cellPositions.Count > 0)
            {
                int index = randomLocation.Next(cellPositions.Count);
                var firstCellLocation = cellPositions.ElementAt(index);
                cellPositions.RemoveAt(index);

                index = randomLocation.Next(cellPositions.Count);
                var secondCellLocation = cellPositions.ElementAt(index);
                cellPositions.RemoveAt(index);

                T value = i_ValueGenerator(valueIndex);
                valueIndex++;
                m_CellArray[firstCellLocation.Item1, firstCellLocation.Item2] = new BoardCell<T>(value);
                m_CellArray[secondCellLocation.Item1, secondCellLocation.Item2] = new BoardCell<T>(value);
            }
        }

        internal void RevealCell(string i_Cell)
        {
            int row = convertCellNameToRowNum(i_Cell);
            int column = convertCellNameToColumnNum(i_Cell);

            m_CellArray[row, column].RevealCell();
        }

        internal void HideCell(string i_Cell)
        {
            int row = convertCellNameToRowNum(i_Cell);
            int column = convertCellNameToColumnNum(i_Cell);

            m_CellArray[row, column].HideCell();
        }

        internal bool IsBoardFullyRevealed()
        {
            bool isRevealed = true;

            for (int row = 0; row < m_NumOfRows; row++)
            {
                for (int col = 0; col < m_NumOfColumns; col++)
                {
                    if (!m_CellArray[row, col].m_IsRevealed)
                    {
                        isRevealed = false;
                    }
                }
            }
            return isRevealed;
        }

        internal bool IsMatchingCells(string i_Cell1, string i_Cell2)
        {
            int row1 = convertCellNameToRowNum(i_Cell1);
            int column1 = convertCellNameToColumnNum(i_Cell1);
            int row2 = convertCellNameToRowNum(i_Cell2);
            int column2 = convertCellNameToColumnNum(i_Cell2);

            return EqualityComparer<T>.Default.Equals(m_CellArray[row1, column1].m_CellValue, m_CellArray[row2, column2].m_CellValue);
        }

        private int convertCellNameToRowNum(string i_Cell)
        {
            return i_Cell[1] - '1';
        }

        private int convertCellNameToColumnNum(string i_Cell)
        {
            return i_Cell[0] - 'A';
        }

        internal bool IsCellRevealed(string i_Cell)
        {
            int row = convertCellNameToRowNum(i_Cell);
            int column = convertCellNameToColumnNum(i_Cell);

            return m_CellArray[row, column].m_IsRevealed;
        }
    }
}
