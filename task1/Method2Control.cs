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
                string path = Validation.СheckFileExist(Console.ReadLine());

                string text = File.ReadAllText(path, Encoding.Default);
                Regex.Replace(text, "[-.?!)(,:]", "").Replace(@"""", "");
                string[] fileTextMass = text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                List<string> listCommaWord = new List<string>();
                for (int i = 9; i < fileTextMass.Length; i += 10)
                    listCommaWord.Add(fileTextMass[i]);

                string commaWords = String.Join(", ", listCommaWord.ToArray());
                Console.WriteLine($"Words in the text: {fileTextMass.Length}");
                Console.WriteLine($"Every 10th word: {commaWords}");
            }
            catch (Exception ex) { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
