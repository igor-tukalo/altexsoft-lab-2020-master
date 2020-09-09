using System;

namespace task2
{
    public static class Validation
    {
        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ValidNumber(string number)
        {
            int numId = 0;
            bool validStart = false;
            while (!validStart)
            {
                //check for number
                bool isNum = int.TryParse(number, out numId);
                if (!string.IsNullOrWhiteSpace(number) && isNum)
                {
                    validStart = true;
                }
                else
                {
                    Console.WriteLine("    Warning! Value must be numeric!");
                    Console.Write("\n    Enter value: ");
                    number = Console.ReadLine();
                }
            }
            return numId;
        }

        /// <summary>
        /// Check string value for number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static double ValidDouble(string number)
        {
            double numId = 0;
            bool validStart = false;
            while (!validStart)
            {
                //check for number
                bool isNum = double.TryParse(number, out numId);
                if (!string.IsNullOrWhiteSpace(number) && isNum)
                {
                    validStart = true;
                }
                else
                {
                    Console.WriteLine("    Warning! Value must be numeric!");
                    Console.Write("\n    Enter value: ");
                    number = Console.ReadLine();
                }
            }
            return numId;
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

                Console.Write("y or n? ");
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.Y && response != ConsoleKey.N); // If the user did not respond with a 'Y' or an 'N', repeat the loop.

            return response;
        }

        /// <summary>
        /// Wrapping text on a new line after n characters
        /// </summary>
        /// <param name="numChar">number of characters in one line</param>
        /// <param name="text">number of characters</param>
        /// <param name="wrapChar">indent mark</param>
        public static string WrapText(int numChar, string text, string wrapChar = "\n")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] wrapText = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                if (wrapText.Length > numChar)
                    for (int i = numChar - 1; i < wrapText.Length; i += numChar)
                    {
                        wrapText[i] += wrapChar;
                    }
                return string.Join(" ", wrapText).Replace(wrapChar + " ", wrapChar);
            }
            else return text;
        }

        /// <summary>
        /// Сheck for text emptiness
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string NullOrEmptyText(string text)
        {
            do
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.Write("    The value cannot be empty! Enter the value: ");
                    text = Console.ReadLine();
                }
            }
            while (string.IsNullOrWhiteSpace(text));
            return text;
        }

        /// <summary>
        /// Check for batch availability
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="countBatch"></param>
        /// <returns></returns>
        public static int BatchExist(string batch, int countBatch)
        {
            int batchNum = ValidNumber(batch);
            do
            {
                if (batchNum < 1 || batchNum > countBatch)
                {
                    Console.Write("    The page number does not exist! Enter page number: ");
                    batchNum = ValidNumber(Console.ReadLine());
                }
            }
            while (batchNum < 1 || batchNum > countBatch);
            return batchNum;
        }
    }
}