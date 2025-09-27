using System;

namespace Ex04.Menus.Test
{
    public class TestOperations
    {
        public static void PrintCurrentDate()
        {
            Console.WriteLine(string.Format("The Date is: {0}", DateTime.Now.ToString("dd/MM/yy")));
            printContinueMessageForUser();
        }

        public static void PrintCurrentTime()
        {
            Console.WriteLine(string.Format("The Hour Is: {0}", DateTime.Now.ToString("HH:mm:ss")));
            printContinueMessageForUser();
        }

        public static void PrintVersion()
        {
            Console.WriteLine("Version: 24.2.4.9504");
            printContinueMessageForUser();
        }

        public static void CountCapitals()
        {
            string userInput;

            Console.Write("Please enter your sentence: ");
            userInput = Console.ReadLine();
            Console.WriteLine(string.Format("There are {0} capitals in your sentence", countCapitalsInSentence(userInput)));
            printContinueMessageForUser();
        }

        private static int countCapitalsInSentence(string i_StringToCount)
        {
            int capitalsCounter = 0;

            foreach (char character in i_StringToCount)
            {
                if (char.IsUpper(character))
                {
                    capitalsCounter++;
                }
            }

            return capitalsCounter;
        }

        private static void printContinueMessageForUser()
        {
            Console.Write("Press Enter To Continue...");
            Console.ReadLine();
        }
    }
}
