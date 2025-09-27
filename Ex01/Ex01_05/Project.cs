using System;

namespace Ex01_05
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
            Console.WriteLine("Welcome! Please input an 8-digit positive whole number:");
            string userInput = Console.ReadLine();

            while (!checkIfInputIsValid(userInput))
            {
                Console.WriteLine("Sorry, the input provided does not meet the requirements. Please enter an 8-digit positive whole number:");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool checkIfInputIsValid(string i_UserInput)
        {
            bool isNumber = int.TryParse(i_UserInput, out _);

            return (isNumber && (i_UserInput.Length == 8));
        }

        private static void printStatistics(string i_UserInput)
        {
            string numberOfDigitsSmallerThanRightMostDigitMsg = string.Format("There are {0} digits that are smaller than the right most digit", numOfCharsSmallerThanRightMostDigit(i_UserInput));
            string numberOfDigitsDivisableByThreeMsg = string.Format("There are {0} digits that are divisable by 3", numOfDigitsDivisableByThree(i_UserInput));
            string largestDigitInTheNumberMsg = string.Format("The largest digit in the number is: {0}", greatestDigit(i_UserInput));
            string digitsAverageMsg = string.Format("The average of the digits in the number is: {0}", digitsAverage(i_UserInput));

            Console.WriteLine(numberOfDigitsSmallerThanRightMostDigitMsg);
            Console.WriteLine(numberOfDigitsDivisableByThreeMsg);
            Console.WriteLine(largestDigitInTheNumberMsg);
            Console.WriteLine(digitsAverageMsg);
            Console.WriteLine("Press Enter To Exit");
            Console.ReadLine();
        }

        private static int numOfCharsSmallerThanRightMostDigit(string i_UserInput)
        {
            char rightMostDigit = i_UserInput[i_UserInput.Length - 1];
            int countSmallerDigits = 0;

            for (int i = 0; i < i_UserInput.Length - 1; i++)
            {
                if (i_UserInput[i] < rightMostDigit)
                {
                    countSmallerDigits++;
                }
            }

            return countSmallerDigits;
        }

        private static int numOfDigitsDivisableByThree(string i_UserInput)
        {
            int intVal;
            int numOfDigitsDivisableByThree = 0;

            foreach (char digit in i_UserInput)
            {
                intVal = digit - '0';
                if (intVal % 3 == 0)
                {
                    numOfDigitsDivisableByThree++;
                }
            }

            return numOfDigitsDivisableByThree;
        }

        private static char greatestDigit(string i_UserInput)
        {
            char maxDigit = i_UserInput[0];

            foreach (char digit in i_UserInput.Substring(1))
            {
                if (digit > maxDigit)
                {
                    maxDigit = digit;
                }
            }

            return maxDigit;
        }

        private static float digitsAverage(string i_UserInput)
        {
            float sumOfDigits = 0;
            int.TryParse(i_UserInput, out int userInputInt);

            while (userInputInt > 0)
            {
                sumOfDigits += userInputInt % 10;
                userInputInt /= 10;
            }

            return sumOfDigits / 8;
        }
    }
}
