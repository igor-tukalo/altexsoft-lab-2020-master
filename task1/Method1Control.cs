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
                string path = Console.ReadLine();

                Console.Write("Enter the path copy file: ");
                string newPath = Console.ReadLine();

                Console.WriteLine("Enter the word: ");
                string word = Console.ReadLine();

                if (File.Exists(path))
                {
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
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }
        }

    }
}
