using System;
using System.IO;
using System.Text;

namespace task1
{
    public class Method1Control
    {
        public void Method1()
        {
            try
            {
                Console.Write("Enter the path original file: ");
                string path = Validation.СheckFileExist(Console.ReadLine());

                Console.Write("Enter the path copy file: ");
                string newPath = Validation.СheckFileExist(Console.ReadLine());

                Console.WriteLine("Enter the word: ");
                string word = Console.ReadLine();

                File.Copy(path, newPath);

                string text = File.ReadAllText(path, Encoding.Default);
                if (text.Contains(word))
                {
                    text = text.Replace(word, "");
                    File.WriteAllText(newPath, text, Encoding.Default);
                    Console.WriteLine($"{word} deleted from file!");
                }
                else
                    Console.WriteLine("The text does not contain the specified word!");
            }
            catch (Exception ex) { Console.WriteLine($"Exception: {ex.Message}"); }
        }

    }
}
