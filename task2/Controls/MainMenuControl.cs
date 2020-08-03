using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using task2.Models;
using Newtonsoft.Json;

namespace task2
{
    public class MainMenuControl
    {
        public void LoadMainMenu()
        {
            Console.WriteLine(@"
                 _.--._  _.--._
            ,-=.-':;:;:;\':;:;:;'-._
            \\\:;:;:;:;:;\:;:;:;:;:;\
             \\\:;:;:;:;:;\:;:;:;:;:;\
              \\\:;:;:;:;:;\:;:;:;:;:;\
               \\\:;:;:;:;:;\:;::;:;:;:\
                \\\;:;::;:;:;\:;:;:;::;:\
                 \\\;;:;:_:--:\:_:--:_;:;\    Welcome to the Cook Book!
                  \\\_.-'      :      ''-.\
                   \`_..--''--.;.--'''--.._\
                    ");

            var filePath = @"D:\MainMenu.json";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            // Read existing json data
            var jsonData = File.ReadAllText(filePath);
            // De-serialize to object or create new list
            var MainMenuList = JsonConvert.DeserializeObject<List<MainMenu>>(jsonData)
                                     ?? new List<MainMenu>();

            foreach (var mainMenu in MainMenuList)
                Console.WriteLine($" {mainMenu.Id}. {mainMenu.Name}");

            Console.Write(" Enter: ");
            Console.ReadLine();
        }
    }
}
