using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Threading.Tasks;

namespace HomeTask4.Cmd
{
    public class ValidationNavigation : IValidationNavigation
    {
        /// <summary>
        /// Obtain a Y or N response
        /// </summary>
        /// <returns>response</returns>
        public Task<ConsoleKey> YesNoAsync()
        {
            ConsoleKey response;
            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                {
                    Console.ReadKey();
                }
                Console.WriteLine("    y or n? ");
                response = Console.ReadKey().Key;
                Console.WriteLine();
            } while (response != ConsoleKey.Y && response != ConsoleKey.N); // If the user did not respond with a 'Y' or an 'N', repeat the loop.

            return Task.FromResult(response);
        }

        /// <summary>
        /// Wrapping text on a new line after n characters
        /// </summary>
        /// <param name="numChar">number of characters in one line</param>
        /// <param name="text">number of characters</param>
        /// <param name="wrapChar">indent mark</param>
        public Task<string> WrapText(int numChar, string text, string wrapChar = "\n")
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                string[] wrapText = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                if (wrapText.Length > numChar)
                {
                    for (int i = numChar - 1; i < wrapText.Length; i += numChar)
                    {
                        wrapText[i] += wrapChar;
                    }
                }
                return Task.FromResult(string.Join(" ", wrapText).Replace(wrapChar + " ", wrapChar));
            }
            else
            {
                return Task.FromResult(text);
            }
        }

        /// <summary>
        /// Сheck for text emptiness
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Task<string> NullOrEmptyText(string text)
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
        /// Check for batch availability
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="countBatch"></param>
        /// <returns></returns>
        public Task<int> BatchExist(int batch, int countBatch)
        {
            try
            {
                do
                {
                    if (batch < 1 || batch > countBatch)
                    {
                        Console.Write("    The page number does not exist! Enter page number: ");
                        batch = int.Parse(Console.ReadLine());
                    }
                }
                while (batch < 1 || batch > countBatch);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return Task.FromResult(batch);
        }
    }
}
