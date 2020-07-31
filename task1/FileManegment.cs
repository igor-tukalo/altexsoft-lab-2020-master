using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using task1.Model;

namespace task1
{
    public class FileManegment
    {
        List<FileInform> fileInfos = new List<FileInform>();

        private string SelectFile(string path)
        {
            Console.WriteLine("Current path: " + path);
            bool validChangepath = false;
            while (validChangepath == false)
            {
                Console.WriteLine("Change path? y/n");
                string answer = Console.ReadLine();
                if (answer == "y" || answer == "Y")
                {
                    Console.Write("Enter the path : ");
                    path = Console.ReadLine();
                    validChangepath = true;
                }
                else if (answer == "n" || answer == "N")
                { validChangepath = true; }
                else Console.WriteLine("\r\nPlease press y or n to choose!");
            }
            return path;
        }


        public void Method1()
        {
            string path = @"D:\Work\Altexsoft\task1\origin.txt";
            path = SelectFile(path);

            Console.WriteLine("Enter the word: ");
            string word = Console.ReadLine();

            string newPath = @"D:\Work\Altexsoft\task1\test.txt";
            try
            {
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(newPath, true);
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        using (StreamWriter sw = new StreamWriter(newPath, false, System.Text.Encoding.Default))
                        {
                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(line.Replace(word, ""));
                                sw.WriteLine(line.Replace(word, ""));
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine("Исключение: " + ex.Message); }
        }

        public void Method2()
        {
            string path = @"D:\Work\Altexsoft\task1\origin.txt";
            path = SelectFile(path);

            StringBuilder fileText = new StringBuilder();

            try
            {
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            fileText.Append(Regex.Replace(line, "[-.?!)(,:]", "").Replace(@"""", "") + " ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine("Exception: " + ex.Message); }

            string[] fileTextMass;
            fileTextMass = fileText.ToString().Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);


            string commaWords = string.Empty;
            for (int i = 9; i < fileTextMass.Length; i += 10)
            {

                commaWords += fileTextMass[i] + ", ";

            }
            Console.WriteLine("Words in the text: " + fileTextMass.Length);
            Console.WriteLine("Every 10th word: " + commaWords.TrimEnd(' ').TrimEnd(','));
        }

        public void Method3()
        {
            string path = @"D:\Work\Altexsoft\task1\origin.txt";
            SelectFile(path);

            StringBuilder fileText = new StringBuilder();

            try
            {
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists)
                {
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            fileText.Append(line + " ");

                        }
                    }
                }
                else
                {
                    Console.WriteLine("File path not found!");
                }
            }
            catch (Exception ex) { Console.WriteLine("Exception: " + ex.Message); }

            string[] fileTextMass;
            fileTextMass = fileText.ToString().Split('.');

            string sentanceWordReverse = string.Empty;
            for (int i = 0; i < fileTextMass.Length; i++)
            {
                if (i == 2)
                {
                    string[] sentence = fileTextMass[i].Trim().Split(' ');

                    for (int s = 0; s < sentence.Length; s++)
                    {
                        sentanceWordReverse += new string(sentence[s].ToCharArray().Reverse().ToArray()) + " ";
                    }
                    break;
                }
            }
            Console.WriteLine(sentanceWordReverse.TrimEnd(' ') + ".");
        }

        public void Method4()
        {
            string dirName = "D:\\";
            dirName = SelectFile(dirName);
            try
            {
                GetDirList(dirName, true);
                Console.WriteLine("\r\n-----------------------------------------");
                Console.WriteLine("Navigation:");
                Console.WriteLine("-----------------------------------------");
                int maxId = fileInfos.Max(x => x.Id) + 1;
                fileInfos.Add(new FileInform() { Id = maxId, Name = "Root", PrevPath = "Change root directory", TypeFile = "Settings" });
                Console.WriteLine("id: " + maxId + "| " + "Change root directory");
                Console.WriteLine("-----------------------------------------");

                bool validStart = false;
                while (validStart == false)
                {

                    Console.Write("\r\nEnter id: ");
                    string id = Console.ReadLine();

                    int NumId = ValidNumber(id);

                    if (fileInfos.Exists(x => x.Id == NumId))
                    {

                        FileInform dirInfo = (from t in fileInfos
                                              where t.Id == NumId
                                              select t).First();

                        if (dirInfo.TypeFile == "File")
                            Console.WriteLine("Only directories can be opened!");
                        else
                            validStart = NextDir(dirInfo.Name, dirInfo.Root);
                    }
                    else
                    {
                        Console.WriteLine("Not found id!");
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }

        private void GetDirList(string dirName, bool isNextDir)
        {
            Console.Clear();
            if (isNextDir)
                fileInfos = new List<FileInform>();
            else
                foreach (var flInf in fileInfos.Where(x => x.TypeFile != "Settings"))
                {
                    fileInfos.Remove(flInf);
                }

            if (Directory.Exists(dirName))
            {

                string[] dirs = Directory.GetDirectories(dirName);
                int id = 0;
                foreach (string s in dirs)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    id++;
                    fileInfos.Add(new FileInform() { Id = id, Name = s, TypeFile = "Catalog", Root = dirInfo.Root.ToString() });
                }

                string[] files = Directory.GetFiles(dirName);

                foreach (string s in files)
                {
                    id++;
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    fileInfos.Add(new FileInform() { Id = id, Name = s, TypeFile = "File", Root = dirInfo.Root.ToString() });
                }

                Console.WriteLine("Catalogs:");
                foreach (var flinf in fileInfos.Where(x => x.TypeFile == "Catalog").OrderBy(x => x.Name))
                {

                    Console.WriteLine("id: " + flinf.Id + "| " + flinf.Name);
                }

                Console.WriteLine("\r\nFiles:");
                foreach (var flinf in fileInfos.Where(x => x.TypeFile == "File").OrderBy(x => x.Name))
                {
                    Console.WriteLine("id: " + flinf.Id + "| " + flinf.Name);
                }
            }
        }

        private bool NextDir(string dirName, string root)
        {
            if (dirName == "Root")
            {
                dirName = SelectFile(dirName);
                root = dirName;
            }
            else if (dirName == "Exit")
                return true;


            string dirNamePrev = string.Empty;
            GetDirList(dirName, true);

            if (dirName.Substring(dirName.Length - 1) == "\\")
            {

                dirNamePrev = dirName.Remove(dirName.Length - 1);
                dirNamePrev = dirNamePrev.Substring(0, dirNamePrev.LastIndexOf('\\') + 1);
            }
            else dirNamePrev = dirName.Substring(0, dirName.LastIndexOf('\\') + 1);

            int maxId = fileInfos.Max(x => x.Id) + 1;
            if (!string.IsNullOrEmpty(dirNamePrev))
                fileInfos.Add(new FileInform() { Id = maxId++, Name = dirNamePrev, PrevPath = "Return to previous directory", TypeFile = "Settings" });
            if (!string.IsNullOrEmpty(root))
                fileInfos.Add(new FileInform() { Id = maxId++, Name = root, PrevPath = "Return to root directory", TypeFile = "Settings" });
            fileInfos.Add(new FileInform() { Id = maxId++, Name = "Root", PrevPath = "Change root directory", TypeFile = "Settings" });
            fileInfos.Add(new FileInform() { Id = maxId++, Name = "Exit", PrevPath = "Exit to the method selection menu", TypeFile = "Settings" });

            Console.WriteLine("\r\n-----------------------------------------");
            Console.WriteLine("Navigation:");
            Console.WriteLine("-----------------------------------------");
            foreach (var dirInf in fileInfos.Where(x => x.TypeFile == "Settings"))
            {
                Console.WriteLine("id: " + dirInf.Id + "| " + dirInf.PrevPath);
                Console.WriteLine("-----------------------------------------");
            }

            Console.WriteLine("\r\nCurrent directory: " + dirName);

            return false;
        }

        private int ValidNumber(string nubmer)
        {
            int NumId;

            bool validStart = false;
            while (validStart == false)
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


    }
}
