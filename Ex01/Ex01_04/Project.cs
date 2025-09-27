using System;

namespace Ex01_04
{
    internal class Project
    {
        public static void Main()
        {
            string userInput = getUserInput();
            printStatistics(userInput);
        }

        private static string getUserInput()
        {
            Console.WriteLine("Hello! please enter a 10 - characters string");
            string userInput = Console.ReadLine();

            while (!checkIfInputIsValid(userInput))
            {
                Console.WriteLine("The input is not valid. please enter a string containing only numbers or only english letters");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool checkIfInputIsValid(string i_UserInput)
        {
            bool isInputValid = true;
            bool isNumber = long.TryParse(i_UserInput, out long _);

            if (!isNumber)
            {
                foreach (char c in i_UserInput)
                {
                    if (!char.IsLetter(c))
                    {
                        isInputValid = false;
                        break;
                    }
                }
            }

            return isInputValid && i_UserInput.Length == 10;
        }

        private static void printNumberStatistics(long i_UserInput)
        {
            if (isDivisableByFour(i_UserInput))
            {
                Console.WriteLine("The input is divisable by four!");
            }
            else
            {
                Console.WriteLine("The input is not divisable by four");
            }
        }

        private static void printStringStatistics(string i_UserInput)
        {
            string numOfLowerCaseLettersMsg = string.Format("The input contains {0} lower case letters", numOfLowerCaseLetters(i_UserInput));

            if (IsPalindrome(i_UserInput))
            {
                Console.WriteLine("The input is a palindrome!");
            }
            else
            {
                Console.WriteLine("The input is not a palindrome");
            }

            Console.WriteLine(numOfLowerCaseLettersMsg);
        }

        private static void printStatistics(string i_UserInputString)
        {
            long userInputLong;

            if (long.TryParse(i_UserInputString, out userInputLong))
            {
                printNumberStatistics(userInputLong);
            }
            else
            {
                printStringStatistics(i_UserInputString);
            }

            Console.WriteLine("Press Enter To Exit");
            Console.ReadLine();
        }

        public static bool IsPalindrome(string i_UserInput)
        {
            int userInputLength = i_UserInput.Length;
            bool isPalindrome = false;

            if (userInputLength == 1 || userInputLength == 0)
            {
                isPalindrome = true;
            }
            else if (i_UserInput[0] == i_UserInput[userInputLength - 1])
            {
                isPalindrome = IsPalindrome(i_UserInput.Substring(1, userInputLength - 2));
            }

            return isPalindrome;
        }
        
        private static bool isDivisableByFour(long i_UserInput)
        {
            return i_UserInput % 4 == 0;
        }

        private static int numOfLowerCaseLetters(string i_UserInput)
        {
            int numOfLowerCase = 0;

            foreach (char letter in i_UserInput)
            {
                if (char.IsLower(letter))
                {
                    numOfLowerCase++;
                }
            }

            return numOfLowerCase;
        }
    }
}