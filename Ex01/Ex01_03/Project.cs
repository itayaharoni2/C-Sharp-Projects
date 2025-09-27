using System;

namespace Ex01_03
{
    class Program
    {
        public static void Main()
        {
            printAsterikRhombus();
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        private static void printAsterikRhombus()
        {
            int heightOfRhombus = getUserInput();

            Ex01_02.Program.printAsterikRhombus(heightOfRhombus);
        }

        private static int getUserInput()
        {
            int intUserInput;
            string stringUserInput;

            Console.WriteLine("Enter a positive number for the height of the desired Asterik Rhombus");
            stringUserInput = Console.ReadLine();

            while (!isValid(stringUserInput, out intUserInput))
            {
                Console.WriteLine("Enter a positive number for the height of the desired Asterik Rhombus");
                stringUserInput = Console.ReadLine();
            }

            if (intUserInput % 2 == 0)
            {
                intUserInput++;
            }

            return intUserInput;
        }

        private static bool isValid(string i_UserInput, out int o_UserInputInt)
        {
            bool isNumber = int.TryParse(i_UserInput, out o_UserInputInt);
            return (isNumber && o_UserInputInt > 0);
        }
    }
}