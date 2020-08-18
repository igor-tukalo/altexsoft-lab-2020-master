using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Controls;
using task2.Models;

namespace task2.Instruments
{
    public class ContextMenuCategories : MenuNavigation
    {
        protected MenuNavigation SwappableObject { get; set; }
        protected List<EntityMenu> EntityList { get; set; }
        protected int IdMenuNavigation { get; set; }

        /// <summary>
        /// Call the action menu for the specified item in the navigation menu
        /// </summary>
        /// <param name="swappableObject">object for which actions will be performed</param>
        /// <param name="jsonFileName"></param>
        /// <param name="idMenuNavigation"></param>
        /// <param name="entityList"></param>
        public ContextMenuCategories(MenuNavigation swappableObject, string jsonFileName, int idMenuNavigation, List<EntityMenu> entityList)
        {
            SwappableObject = swappableObject;
            Console.Clear();
            EntityList = entityList;
            jsonControl = new JsonControl(jsonFileName);
            IdMenuNavigation = idMenuNavigation;
            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "Rename"),
                new Category(name: "Delete"),
                new Category(name: "Cancel")
            };

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine();
                        foreach (var item in EntityList.Where(x => x.Id == IdMenuNavigation))
                        {
                            item.Name = newName;
                        }
                        File.WriteAllText(jsonControl.GetJsonPathFile(), JsonConvert.SerializeObject(EntityList));
                        SwappableObject.GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            EntityList.Remove(EntityList.FirstOrDefault(x => x.Id == IdMenuNavigation));
                            File.WriteAllText(jsonControl.GetJsonPathFile(), JsonConvert.SerializeObject(EntityList));
                            SwappableObject.GetMenuItems();
                        }
                        else SwappableObject.GetMenuItems();

                    }
                    break;
                case 2:
                    {
                        SwappableObject.GetMenuItems();
                    }
                    break;
            }
        }


    }
}
