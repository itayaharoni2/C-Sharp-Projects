using System;
using System.Text;

namespace Ex01_02
{
   public class Program
    {
        public static void Main()
        {
            printAsterikRhombus();
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        public static void printAsterikRhombus(int RhombusHeight = 9)
        {
            StringBuilder asterikBuilder = new StringBuilder();

            asterikRhombusCreator(ref asterikBuilder, RhombusHeight);
            Console.WriteLine(asterikBuilder);
        }

        private static StringBuilder asterikRhombusCreator(ref StringBuilder io_AsterikRhombus, int i_HeightOfRhombus, int i_RecursionHeight = 0)
        {
            if (i_HeightOfRhombus == 1)
            {
                string numberOfAsterik = new string('*', (i_RecursionHeight * 2) + 1);
                io_AsterikRhombus.AppendLine(numberOfAsterik);
            }

            else
            {
                string numberOfSpaces = new string(' ', i_HeightOfRhombus / 2);
                string numberOfAsterik = new string('*', (i_RecursionHeight * 2) + 1);
                string rowOfRhombus = string.Format("{0}{1}", numberOfSpaces, numberOfAsterik);
                io_AsterikRhombus.AppendLine(rowOfRhombus);
                asterikRhombusCreator(ref io_AsterikRhombus, i_HeightOfRhombus - 2, i_RecursionHeight + 1);
                io_AsterikRhombus.AppendLine(rowOfRhombus);
            }

            return io_AsterikRhombus;
        }
    }
}