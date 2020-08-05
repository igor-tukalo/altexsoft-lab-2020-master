using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace task1
{
    public class Method2Control
    {
        public void Method2()
        {
            try
            {
                Console.Write("Enter the path file: ");
                string path = Console.ReadLine();

                StringBuilder fileText = new StringBuilder();

                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path, Encoding.Default);
                    fileText.Append(Regex.Replace(text, "[-.?!)(,:]", "").Replace(@"""", "") + " ");
                    string[] fileTextMass = fileText.ToString().Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                    List<string> listCommaWord = new List<string>();
                    for (int i = 9; i < fileTextMass.Length; i += 10)
                        listCommaWord.Add(fileTextMass[i]);

                    string commaWords = String.Join(", ", listCommaWord.ToArray());
                    Console.WriteLine($"Words in the text: {fileTextMass.Length}");
                    Console.WriteLine($"Every 10th word: {commaWords}");
                }
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
