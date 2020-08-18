using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Instruments;
using task2.Models;

namespace task2.Controls
{
    public class CategoryControl : MenuNavigation
    {
        protected List<Category> CategoriesList { get; set; }
        public CategoryControl()
        {
            jsonControl = new JsonControl("Categories.json");
            //De - serialize to object or create new list
            CategoriesList = JsonConvert.DeserializeObject<List<Category>>(jsonControl.GetJsonData());
        }
        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(jsonControl.JsonFileName))
            {
                ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Add category"),
                    new Category(name: "    Return to settings"),
                    new Category(name: "    Return to main menu")
                };

                var parent = CategoriesList.Find((x) => x.Id == IdMenu);
                BuildHierarchicalMenu(new List<EntityMenu>(CategoriesList), parent, 1);

            }
            else Console.WriteLine("    There are no recipe categories! Add categories!");

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        AddMenuItem(jsonControl.GetJsonPathFile());
                    }
                    break;
                case 1:
                    {
                        SettingsControl settingsControl = new SettingsControl();
                        settingsControl.GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        MainMenuControl mainMenuControl = new MainMenuControl();
                        mainMenuControl.GetMenuItems();
                    }
                    break;
                // when selecting categories, we call the context menu of actions
                default:
                    {
                        if(ItemsMenu[id].Id!=1)
                        _ = new ContextMenuCategories(new CategoryControl() ,jsonControl.JsonFileName, ItemsMenu[id].Id, new List<EntityMenu>(CategoriesList));
                    }
                    break;
            }
        }

        protected override void AddMenuItem(string filePath)
        {
            try
            {
                Console.WriteLine();
                int id = CategoriesList.Max(x => x.Id) + 1;
                Console.Write(" Enter name category: ");
                string name = Console.ReadLine();
                Console.Write(" Enter name main category: ");
                string nameMainCategory = Console.ReadLine();
                int idMainCategory = 0;
                do
                {
                    if (CategoriesList.Exists(x => x.Name == nameMainCategory))
                    {
                        idMainCategory = (from t in CategoriesList
                                          where t.Name == nameMainCategory
                                          select t.Id).First();
                    }
                    else
                    {
                        Console.Write(" No main category name found. Enter an existing name: ");
                        nameMainCategory = Console.ReadLine();
                    }
                }
                while (!CategoriesList.Exists(x => x.Name == nameMainCategory));

                //Add any new category
                CategoriesList.Add(new Category(id, name, idMainCategory));

                Validation.SaveSelectedDataJson(categories: CategoriesList);
                new CategoryControl().GetMenuItems();
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
