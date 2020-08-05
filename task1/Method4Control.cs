using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task1.Model;

namespace task1
{
    public class Method4Control
    {
        List<FileInform> FileInfos { get; set; }
        public void Method4()
        {
            try
            {
                Console.Write("Enter the path root directory: ");
                string dirName = Console.ReadLine();

               FileInfos = GetDirList(dirName);

                Console.WriteLine("\r\n-----------------------------------------");
                Console.WriteLine("Navigation:");
                Console.WriteLine("-----------------------------------------");
                int maxId = FileInfos.Max(x => x.Id) + 1;
                FileInfos.Add(new FileInform() { Id = maxId, Name = "Root", PrevPath = "Change root directory", TypeFile = "Settings" });
                Console.WriteLine($"id: {maxId} | Change root directory");
                Console.WriteLine("-----------------------------------------");

                bool validStart = false;
                while (!validStart)
                {

                    Console.Write("\r\nEnter id: ");
                    string id = Console.ReadLine();

                    int NumId = Validation.ValidNumber(id);

                    if (FileInfos.Exists(x => x.Id == NumId))
                    {

                        FileInform dirInfo = (from t in FileInfos
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

        /// <summary>
        /// Get list of directories and files
        /// </summary>
        /// <param name="dirName">Directory path</param>
        private List<FileInform> GetDirList(string dirName)
        {
            Console.Clear();
            List<FileInform> fileInfos = new List<FileInform>();
            if (Directory.Exists(dirName))
            {

                string[] dirs = Directory.GetDirectories(dirName);
                int id = 0;
                foreach (string s in dirs.OrderBy(x=>x.ToString()))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    id++;
                    fileInfos.Add(new FileInform() { Id = id, Name = s, TypeFile = "Catalog", Root = dirInfo.Root.ToString() });
                }

                string[] files = Directory.GetFiles(dirName);

                foreach (string s in files.OrderBy(x => x.ToString()))
                {
                    id++;
                    DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                    fileInfos.Add(new FileInform() { Id = id, Name = s, TypeFile = "File", Root = dirInfo.Root.ToString() });
                }

                Console.WriteLine("Catalogs:");
                foreach (var flinf in fileInfos.Where(x => x.TypeFile == "Catalog").OrderBy(x => x.Name))
                {

                    Console.WriteLine($"id: {flinf.Id} | {flinf.Name}");
                }

                Console.WriteLine("\r\nFiles:");
                foreach (var flinf in fileInfos.Where(x => x.TypeFile == "File").OrderBy(x => x.Name))
                {
                    Console.WriteLine($"id: {flinf.Id} | {flinf.Name}");
                }
            }
            return fileInfos;
        }

        /// <summary>
        /// Follow the specified path
        /// </summary>
        /// <param name="dirName">Directory path</param>
        /// <param name="root">Root path</param>
        /// <returns>true - stop method, false - continue method</returns>
        private bool NextDir(string dirName, string root)
        {
            try
            {
                if (dirName == "Root")
                {
                    Console.Write("Enter the path root directory: ");
                    dirName = Console.ReadLine();
                    root = dirName;
                }
                else if (dirName == "Exit")
                    return true;
                FileInfos = GetDirList(dirName);

                string dirNamePrev = string.Empty;

                //Get prev directory
                if (dirName.Substring(dirName.Length - 1) == "\\")
                {

                    dirNamePrev = dirName.Remove(dirName.Length - 1);
                    dirNamePrev = dirNamePrev.Substring(0, dirNamePrev.LastIndexOf('\\') + 1);
                }
                else dirNamePrev = dirName.Substring(0, dirName.LastIndexOf('\\') + 1);

                int maxId = FileInfos.Max(x => x.Id) + 1;
                if (!string.IsNullOrEmpty(dirNamePrev))
                    FileInfos.Add(new FileInform() { Id = maxId++, Name = dirNamePrev, PrevPath = "Return to previous directory", TypeFile = "Settings" });
                if (!string.IsNullOrEmpty(root))
                    FileInfos.Add(new FileInform() { Id = maxId++, Name = root, PrevPath = "Return to root directory", TypeFile = "Settings" });
                FileInfos.Add(new FileInform() { Id = maxId++, Name = "Root", PrevPath = "Change root directory", TypeFile = "Settings" });
                FileInfos.Add(new FileInform() { Id = maxId++, Name = "Exit", PrevPath = "Exit to the method selection menu", TypeFile = "Settings" });

                Console.WriteLine("\r\n-----------------------------------------");
                Console.WriteLine("Navigation:");
                Console.WriteLine("-----------------------------------------");
                foreach (var dirInf in FileInfos.Where(x => x.TypeFile == "Settings"))
                {
                    Console.WriteLine($"id:{dirInf.Id} | {dirInf.PrevPath}");
                    Console.WriteLine("-----------------------------------------");
                }

                Console.WriteLine("\r\nCurrent directory: " + dirName);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Exception: {ex} \r\n"); 
                MenuControl menuControl = new MenuControl();
                menuControl.CallMenuSelectMethods();
            }
            return false;
        }
    }
}
