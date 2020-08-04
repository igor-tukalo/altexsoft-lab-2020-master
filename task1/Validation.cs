using System;
using System.Collections.Generic;
using System.Text;

namespace task1
{
    public static class Validation
    {
        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="nubmer"></param>
        /// <returns></returns>
        public static int ValidNumber(string nubmer)
        {
            int NumId;

            bool validStart = false;
            while (!validStart)
            {
                //check for number
                bool isNum = int.TryParse(nubmer, out NumId);
                if (!string.IsNullOrWhiteSpace(nubmer) && isNum)
                {
                    return NumId;
                }
                else
                {
                    Console.WriteLine("Warning! Value must be numeric!");
                    Console.Write("\r\nEnter value: ");
                    nubmer = Console.ReadLine();
                }
            }
            return 0;
        }

        /// <summary>
        /// File path check
        /// </summary>
        /// <param name="path"></param>
        /// <returns>path to the file</returns>
        public static string SelectFile(string path)
        {
            Console.WriteLine("Current path: " + path);
            bool validChangepath = false;
            while (validChangepath == false)
            {
                Console.WriteLine("Change path? y/n");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "y")
                {
                    Console.Write("Enter the path : ");
                    path = Console.ReadLine();
                    validChangepath = true;
                }
                else if (answer.ToLower() == "n")
                { validChangepath = true; }
                else Console.WriteLine("\r\nPlease press y or n to choose!");
            }
            return path;
        }

    }
}
