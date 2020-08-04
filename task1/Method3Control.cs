using System;
using System.IO;
using System.Linq;
using System.Text;

namespace task1
{
    public class Method3Control
    {
        public void Method3()
        {
            try
            {
                Console.Write("Enter the path file: ");
                string path = Console.ReadLine();
                Console.WriteLine();

                if (File.Exists(path))
                {
                    string[] fileTextArr = new string(File.ReadAllText(path, Encoding.Default)).ToString().Split(new[] { '.' }).ToArray();

                    string[] wordsReverseArr = fileTextArr[2].Split(' ');

                    for (int i = 0; i < wordsReverseArr.Length; i++)
                    {
                        wordsReverseArr[i] = new string(wordsReverseArr[i].Reverse().ToArray());
                    }
                    Console.WriteLine(string.Join(" ", wordsReverseArr));
                }
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine("Exception: " + ex.Message); }
        }
    }
}
