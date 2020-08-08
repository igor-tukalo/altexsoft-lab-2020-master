using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace task1
{
    public static class Validation
    {
        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ValidNumber(string text)
        {
            //check for number
            int Num;
            bool isNum;
            do
            {
                isNum = int.TryParse(text, out Num);
                if (!isNum)
                {
                    Console.WriteLine("Warning! Value must be numeric!");
                    Console.Write("\r\nEnter value: ");
                    text = Console.ReadLine();
                }
            }
            while (!isNum);
            return Num;
        }

        public static string СheckFileExist(string path)
        {
            while (!File.Exists(path))
            {
                Console.Write("File path not found! Enter the path : ");
                path = Console.ReadLine();
            }
            return path;
        }

        /// <summary>
        /// Obtain a Y or N response
        /// </summary>
        /// <returns>response</returns>
        public static ConsoleKey YesNo()
        {
            ConsoleKey response;

            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                Console.Write("y or n?"); 
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.Y && response != ConsoleKey.N); // If the user did not respond with a 'Y' or an 'N', repeat the loop.

            return response;
        }

    }
}
