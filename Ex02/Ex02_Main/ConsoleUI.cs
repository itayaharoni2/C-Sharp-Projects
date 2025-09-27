using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex02_Memory_Game
{
    internal class ConsoleUI
    {
        private GameLogicManager<char> m_GameLogicManager;
        internal readonly Dictionary<string, string> r_ConsoleUserMessages;

        public ConsoleUI()
        {
            r_ConsoleUserMessages = new Dictionary<string, string>
            {
                { "WelcomeMsg", "Hello! Welcome to the Memory Game!\nIn this game, you will get a bunch of cards facing down,\nand you'll get points for finding the matching pairs!\n" },
                { "AskIfPCPlayer", "Do you want to play against the PC? (yes/no)" },
                { "InvalidIsPCQuestionInput", "Invalid input. Please enter 'yes' or 'no': " },
                { "EnterPlayer1Name", "Please enter Player 1's name" },
                { "EnterPlayer2Name", "Please enter Player 2's name" },
                { "InvalidUserName", "Invalid username. Please enter a name with 20 characters or less and no spaces." },
                { "CellChoice", "please enter your cell choice (e.g., A1) or 'Q' to exit: " },
                { "InvalidCellChoice", "Please enter a valid cell choice (e.g., A1) or 'Q' to exit: " },
                { "EnterNumRows", "Enter the number of Rows: " },
                { "EnterNumColumns", "Enter the number of columns: " },
                { "InvalidNumberOfCells", "The numbers of rows and columns needs to be between 4 and 6, with total of even number of cells" },
                { "KeepPlaying", "Would you like to keep playing? (yes/no)" },
                { "ThankYou", "Thank you for playing!" },
                { "Winner", "is the winner!" }
            };
        }

        internal void StartGame()
        {
            bool keepPlaying = true;
            const bool v_isPlayerOne = true;
            const bool v_isPC = true;
            PlayerData[] gamePlayers = new PlayerData[2];

            displayMessage("WelcomeMsg");
            while (keepPlaying)
            {
                string firstPlayerName = getUserName(v_isPlayerOne);
                string secondPlayerName;
                if (isPcPlayer())
                {
                    secondPlayerName = "PC";
                    m_GameLogicManager = new GameLogicManager<char>(firstPlayerName, secondPlayerName, v_isPC);
                }
                else
                {
                    secondPlayerName = getUserName(!v_isPlayerOne);
                    m_GameLogicManager = new GameLogicManager<char>(firstPlayerName, secondPlayerName, !v_isPC);
                }

                int numOfRows = getNumOfRows();
                int numOfColumns = getNumOfColumns();

                while (!GameLogicManager<char>.IsDimensionsValid(numOfRows, numOfColumns))
                {
                    displayMessage("InvalidNumberOfCells");
                    numOfRows = getNumOfRows();
                    numOfColumns = getNumOfColumns();
                }

                m_GameLogicManager.InitBoard(numOfRows, numOfColumns, valueGenerator);
                while (!m_GameLogicManager.IsBoardFullyRevealed())
                {
                    if (m_GameLogicManager.CheckIfCurrentPlayerIsPC())
                    {
                        playNextPcTurn();
                    }
                    else
                    {
                        playNextUserTurn();
                    }
                }

                displayWinner();
                keepPlaying = this.keepPlaying();
            }

            displayMessage("ThankYou");
        }

        private void playNextUserTurn()
        {
            printBoard(m_GameLogicManager.m_Board);
            string firstCellChoice = getCellChoice();
            m_GameLogicManager.RevealCell(firstCellChoice);
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(m_GameLogicManager.m_Board);
            string secondCellChoice = getCellChoice();
            m_GameLogicManager.RevealCell(secondCellChoice);
            Ex02.ConsoleUtils.Screen.Clear();
            printBoard(m_GameLogicManager.m_Board);
            if (m_GameLogicManager.IsMatchingCells(firstCellChoice, secondCellChoice))
            {
                m_GameLogicManager.AddPoint();
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
                m_GameLogicManager.HideCell(firstCellChoice);
                m_GameLogicManager.HideCell(secondCellChoice);
                Ex02.ConsoleUtils.Screen.Clear();
                m_GameLogicManager.SwitchCurrentPlayer();
            }
        }

        private void playNextPcTurn()
        {
            printBoard(m_GameLogicManager.m_Board);
            System.Threading.Thread.Sleep(2000);
            string[] currentSelections = m_GameLogicManager.GetPcCellSelection();
            m_GameLogicManager.RevealCell(currentSelections[0]);
            printBoard(m_GameLogicManager.m_Board);
            System.Threading.Thread.Sleep(2000);
            m_GameLogicManager.RevealCell(currentSelections[1]);
            printBoard(m_GameLogicManager.m_Board);
            if (!m_GameLogicManager.IsMatchingCells(currentSelections[0], currentSelections[1]))
            {
                System.Threading.Thread.Sleep(2000);
                m_GameLogicManager.HideCell(currentSelections[0]);
                m_GameLogicManager.HideCell(currentSelections[1]);
                Ex02.ConsoleUtils.Screen.Clear();
                m_GameLogicManager.SwitchCurrentPlayer();
            }
            else
            {
                m_GameLogicManager.AddPoint();
            }
        }

        private bool isPcPlayer()
        {
            displayMessage("AskIfPCPlayer");
            string userAnswer = Console.ReadLine();
            while (userAnswer != "yes" && userAnswer != "no")
            {
                displayMessage("InvalidIsPCQuestionInput");
                userAnswer = Console.ReadLine();
            }

            return userAnswer == "yes";
        }

        private string getUserName(bool i_IsPlayer1)
        {
            displayMessage(i_IsPlayer1 ? "EnterPlayer1Name" : "EnterPlayer2Name");
            string userName = Console.ReadLine();
            while (!isUserNameValid(userName))
            {
                displayMessage("InvalidUserName");
                userName = Console.ReadLine();
            }

            return userName;
        }

        private static bool isUserNameValid(string i_UserName)
        {
            return i_UserName.Length <= 20 && !i_UserName.Contains(' ');
        }

        private string getCellChoice()
        {
            Console.Write(m_GameLogicManager.GetCurrentPlayerName() + ", ");
            displayMessage("CellChoice");
            string currentChoice = Console.ReadLine();
            if (currentChoice == "Q")
            {
                exitGame();
            }

            while (!m_GameLogicManager.IsCellChoiceValid(currentChoice))
            {
                if (currentChoice == "Q")
                {
                    exitGame();
                }

                displayMessage("InvalidCellChoice");
                currentChoice = Console.ReadLine();
            }

            return currentChoice;
        }

        private int getNumOfRows()
        {
            displayMessage("EnterNumRows");

            return int.Parse(Console.ReadLine());
        }

        private int getNumOfColumns()
        {
            displayMessage("EnterNumColumns");

            return int.Parse(Console.ReadLine());
        }

        private void displayWinner()
        {
            Console.Write(m_GameLogicManager.GetCurrentPlayerName() + " ");
            displayMessage("Winner");
        }

        private bool keepPlaying()
        {
            displayMessage("KeepPlaying");
            string userAnswer = Console.ReadLine();
            while (userAnswer != "yes" && userAnswer != "no")
            {
                displayMessage("InvalidIsPCQuestionInput");
                userAnswer = Console.ReadLine();
            }

            return userAnswer == "yes";
        }

        private void exitGame()
        {
            displayMessage("ThankYou");
            System.Threading.Thread.Sleep(2000);
            Environment.Exit(0);
        }

        private void printBoard(GameBoard<char> io_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            for (int j = 0; j < io_Board.m_CellArray.GetLength(1); j++)
            {
                Console.Write("   ");
                Console.Write((char)('A' + j));
            }

            Console.WriteLine();
            Console.WriteLine(" " + new string('=', (io_Board.m_CellArray.GetLength(1) * 4) + 1));
            for (int i = 0; i < io_Board.m_CellArray.GetLength(0); i++)
            {
                Console.Write(i + 1);
                for (int j = 0; j < io_Board.m_CellArray.GetLength(1); j++)
                {
                    if (io_Board.m_CellArray[i, j].m_IsRevealed)
                    {
                        Console.Write("| " + io_Board.m_CellArray[i, j].m_CellValue + " ");
                    }
                    else
                    {
                        Console.Write("|   ");
                    }
                }

                Console.Write("|");
                Console.WriteLine();
                Console.WriteLine(" " + new string('=', (io_Board.m_CellArray.GetLength(1) * 4) + 1));
            }
        }

        private void displayMessage(string io_MessageKey)
        {
            if (r_ConsoleUserMessages.ContainsKey(io_MessageKey))
            {
                Console.WriteLine(r_ConsoleUserMessages[io_MessageKey]);
            }
        }

        private static char valueGenerator(int i_Index)
        {
            return (char)('A' + i_Index);
        }
    }
}
