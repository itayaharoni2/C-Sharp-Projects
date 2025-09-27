using System;

namespace Ex01_01
{
    internal class Program
    {
        public static void Main()
        {
            string[] stringNumbersInputArray = new string[3];

            getUserInput(stringNumbersInputArray);
            printStatistics(stringNumbersInputArray);
        }

        private static void getUserInput(string[] o_StringNumbersInputArray)
        {
            string currentUserInputBeforeCheck;

            Console.WriteLine("hello! enter 3 binary positive numbers with 9 digits each (press Enter after each number)");
            for (int i = 0; i < 3; i++)
            {
                currentUserInputBeforeCheck = Console.ReadLine();
                while (!checkIfInputIsValid(currentUserInputBeforeCheck))
                {
                    Console.WriteLine("the number inserted is not according to the rules. please enter a valid number.");
                    currentUserInputBeforeCheck = Console.ReadLine();
                }

                o_StringNumbersInputArray[i] = currentUserInputBeforeCheck;
            }
        }

        private static bool checkIfInputIsValid(string i_Input)
        {
            bool isInputValid = true;

            isInputValid = (i_Input.Length == 9);

            foreach(char c in i_Input)
            {
                if (c != '0' && c != '1')
                {
                    isInputValid = false;
                }
            }

            return isInputValid;
        }

        private static int[] getDecimalArray(string[] i_StringNumbersInputArray)
        {
            int[] decimalNumbersArray = new int[i_StringNumbersInputArray.Length];
            for (int i = 0; i < i_StringNumbersInputArray.Length; i++)
            {
                decimalNumbersArray[i] = binaryToDecimalNumber(i_StringNumbersInputArray[i]);
            }

            return decimalNumbersArray;
        }

        private static int binaryToDecimalNumber(string i_BinaryNum)
        {
            double positionValue = 0;
            int decimalSum = 0;
            int currentDigit;

            for (int i = i_BinaryNum.Length - 1; i >= 0; i--)
            {
                currentDigit = i_BinaryNum[i] - '0';
                decimalSum += (currentDigit * ((int)Math.Pow(2.0, positionValue)));
                positionValue++;
            }

            return decimalSum;
        }

        private static int[] sortDecimalNumbersArray(string[] i_StringNumbersInputArray)
        {
            int[] sortedDecimalArray = getDecimalArray(i_StringNumbersInputArray);

            // sort the array using BubbleSort
            for (int i = 0; i < sortedDecimalArray.Length - 1; i++)
            {
                for (int j = 0; j < sortedDecimalArray.Length - i - 1; j++)
                {
                    if (sortedDecimalArray[j] > sortedDecimalArray[j + 1])
                    {
                        int x = sortedDecimalArray[j];
                        sortedDecimalArray[j] = sortedDecimalArray[j + 1];
                        sortedDecimalArray[j + 1] = x;
                    }
                }
            }

            return sortedDecimalArray;
        }

        private static int getCountOfMatchingDigits(string i_NumFromInput, bool i_CountZeros)
        {
            int numberOfMatchingDigits = 0;

            foreach (char digit in i_NumFromInput)
            {
                if ((i_CountZeros && digit == '0') || (!i_CountZeros && digit == '1'))
                {
                    numberOfMatchingDigits++;
                }
            }

            return numberOfMatchingDigits; 
        }

        private static int getAverageOfDigits(string[] i_StringNumbersInputArray, bool i_IsCountingZeros)
        {
            int numOfRequestedDigits = 0;

            for (int i = 0; i < i_StringNumbersInputArray.Length; i++)
            {
                numOfRequestedDigits += getCountOfMatchingDigits(i_StringNumbersInputArray[i], i_IsCountingZeros);
            }
            
            //Calculating the average depending on the fact that we have 3 numbers
            return numOfRequestedDigits / 3;
        }

        private static int countNumsThatPowerOfTwo(string[] i_StringNumbersInputArray)
        {
            int countPowerOfTwo = 0;
            const bool v_countZeros = true;

            foreach(string NumInArray in i_StringNumbersInputArray)
            {
                if(getCountOfMatchingDigits(NumInArray, !v_countZeros) == 1)
                {
                    countPowerOfTwo++;
                }
            }

            return countPowerOfTwo;
        }

        private static int countAscendingSeries(int[] sortedDecimalNumbersArray)
        {
            int countOfAccendingNumbers = 0;

            foreach (int numInArray in sortedDecimalNumbersArray)
            {
                if (IsStrictlyAscendingNumber(numInArray))
                {
                    countOfAccendingNumbers++;
                }
            }

            return countOfAccendingNumbers;
        }

        private static bool IsStrictlyAscendingNumber(int i_Number)
        {
            bool isStrictlyAscending = true;
            string numberString = i_Number.ToString();
            
            for (int i = 0; i < numberString.Length - 1; i++)
            {
                if (numberString[i] >= numberString[i + 1])
                {
                    isStrictlyAscending = false;
                }
            }

            return isStrictlyAscending;
        }

        private static void printStatistics(string[] i_StringNumbersInputArray)
        {
            const bool v_countZeros = true;
            int[] sortedDecimalNumbersArray = sortDecimalNumbersArray(i_StringNumbersInputArray);

            string theNumbersInAscendingOrder = string.Format("The numbers in decimal representation in ascending order are: {0}, {1}, {2}", sortedDecimalNumbersArray[0], sortedDecimalNumbersArray[1], sortedDecimalNumbersArray[2]);
            string countPow2NumbersMsg = string.Format("The amount of numbers that are power of two: {0}", countNumsThatPowerOfTwo(i_StringNumbersInputArray));
            string averageNumOfZerosMsg = string.Format("The average amount of zero's: {0}", getAverageOfDigits(i_StringNumbersInputArray, v_countZeros));
            string averageNumOfOnesMsg = string.Format("The average amount of one's: {0}", getAverageOfDigits(i_StringNumbersInputArray, !v_countZeros));
            string countStrictlyAscendingSeriesNumbersMsg = string.Format("The amount of strictly ascending series numbers: {0}", countAscendingSeries(sortedDecimalNumbersArray));
            string whichNumAreMixMax = string.Format("The minimum number is: {0} , and the maximum number is {1}", sortedDecimalNumbersArray[0], sortedDecimalNumbersArray[2]);

            Console.WriteLine(theNumbersInAscendingOrder);
            Console.WriteLine(countPow2NumbersMsg);
            Console.WriteLine(averageNumOfZerosMsg);
            Console.WriteLine(averageNumOfOnesMsg);
            Console.WriteLine(countStrictlyAscendingSeriesNumbersMsg);
            Console.WriteLine(whichNumAreMixMax);
            Console.WriteLine("Press Enter To Exit");
            Console.ReadLine();
        }
    }
}
