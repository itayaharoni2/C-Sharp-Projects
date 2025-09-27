using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02_Memory_Game
{
    internal class PcPlayerLogic<T> where T : struct
    {
        private PlayerData playerData;
        private Dictionary<T, List<Tuple<int, int>>> rememberedCells;

        internal PcPlayerLogic(PlayerData playerLogic)
        {
            playerData = playerLogic;
            playerData.m_IsPC = true;
            rememberedCells = new Dictionary<T, List<Tuple<int, int>>>();
        }

        internal string[] GetPcCellSelection(GameBoard<T> board)
        {
            Console.WriteLine("entered Pc");
            for (int row = 0; row < board.m_CellArray.GetLength(0); row++)
            {
                for (int col = 0; col < board.m_CellArray.GetLength(1); col++)
                {
                    var cell = board.m_CellArray[row, col];
                    if (cell.m_WasRevealed && !cell.m_IsRevealed)
                    {
                        if (!rememberedCells.ContainsKey(cell.m_CellValue))
                        {
                            rememberedCells[cell.m_CellValue] = new List<Tuple<int, int>>();
                        }

                        if (!rememberedCells[cell.m_CellValue].Any(pos => pos.Item1 == col && pos.Item2 == row))
                        {
                            rememberedCells[cell.m_CellValue].Add(Tuple.Create(col, row));
                        }
                    }
                }
            }

            foreach (var entry in rememberedCells)
            {
                if (entry.Value.Count >= 2)
                {
                    var locations = entry.Value;
                    string cell1 = $"{(char)('A' + locations[0].Item1)}{locations[0].Item2 + 1}";
                    string cell2 = $"{(char)('A' + locations[1].Item1)}{locations[1].Item2 + 1}";
                    rememberedCells.Remove(entry.Key);
                  
                    return new string[] { cell1, cell2 };
                }
            }

            Random random = new Random();
            List<Tuple<int, int>> unseenCells = new List<Tuple<int, int>>();

            for (int row = 0; row < board.m_CellArray.GetLength(0); row++)
            {
                for (int col = 0; col < board.m_CellArray.GetLength(1); col++)
                {
                    if (!board.m_CellArray[row, col].m_WasRevealed)
                    {
                        unseenCells.Add(Tuple.Create(col, row));
                    }
                }
            }

            var firstRandomCell = unseenCells[random.Next(unseenCells.Count)];
            unseenCells.Remove(firstRandomCell);

            if (rememberedCells.TryGetValue(board.m_CellArray[firstRandomCell.Item2, firstRandomCell.Item1].m_CellValue, out var matchingCells) &&
                matchingCells.Count > 0)
            {
                var matchingCell = matchingCells.First();
                string cell1 = $"{(char)('A' + firstRandomCell.Item1)}{firstRandomCell.Item2 + 1}";
                string cell2 = $"{(char)('A' + matchingCell.Item1)}{matchingCell.Item2 + 1}";
                
                return new string[] { cell1, cell2 };
            }

            var secondRandomCell = unseenCells[random.Next(unseenCells.Count)];
            string randomCell1 = $"{(char)('A' + firstRandomCell.Item1)}{firstRandomCell.Item2 + 1}";
            string randomCell2 = $"{(char)('A' + secondRandomCell.Item1)}{secondRandomCell.Item2 + 1}";
            
            return new string[] { randomCell1, randomCell2 };
        }
    }
}