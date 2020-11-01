using HomeTask4.Cmd.Interfaces;
using System;
using System.Threading.Tasks;

namespace HomeTask4.Cmd
{
    public class ConsoleHelper : IConsoleHelper
    {
        /// <summary>
        /// Obtain a Y or N response.
        /// </summary>
        /// <returns>response</returns>
        public Task<ConsoleKey> ShowYesNoAsync()
        {
            ConsoleKey response;
            do
            {
                while (Console.KeyAvailable)
                {
                    Console.ReadKey();
                }
                Console.Write("    y or n? ");
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);
            return Task.FromResult(response);
        }

        /// <summary>
        /// Wrapping text on a new line after n characters.
        /// </summary>
        /// <param name="numberChar">number of characters in one line</param>
        /// <param name="text">wrapping text</param>
        /// <param name="wrapChar">indent mark</param>
        /// <returns>wrapping text</returns>
        public Task<string> WrapTextAsync(int numberChar, string text, string wrapChar = "\n")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] wrapText = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                if (wrapText.Length > numberChar)
                {
                    for (int i = numberChar - 1; i < wrapText.Length; i += numberChar)
                    {
                        wrapText[i] += wrapChar;
                    }
                }
                return Task.FromResult(string.Join(" ", wrapText).Replace(wrapChar + " ", wrapChar));
            }
            return Task.FromResult(text);
        }

        /// <summary>
        /// Check text for emptiness.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>text</returns>
        public Task<string> CheckNullOrEmptyTextAsync(string text)
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
            return Task.FromResult(text);
        }

        /// <summary>
        /// Check batch for availability among all batches.
        /// </summary>
        /// <param name="numberBatch">batch number to check</param>
        /// <param name="countBatch">total batch</param>
        /// <returns>number batch</returns>
        public Task<int> BatchExistAsync(int numberBatch, int countBatch)
        {
            try
            {
                while (numberBatch < 1 || numberBatch > countBatch)
                {
                    Console.Write("    The page number does not exist! Enter page number: ");
                    numberBatch = int.Parse(Console.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Task.FromResult(numberBatch);
        }
    }
}
