using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Controls;
using task2.Instruments;
using task2.Models;

namespace task2
{
    public class IngredientControl : MenuNavigation
    {
        protected List<Ingredient> IngredientsList { get; set; }
        public IngredientControl()
        {
            jsonControl = new JsonControl("Ingredients.json");
            ItemsMenuMain = new List<EntityMenu>
                {
                    new Ingredient(name: "  Add ingredient"),
                    new Ingredient(name: "  Return to settings"),
                    new Ingredient(name: "  Return to main menu"),
                    new Ingredient(name: "\n  Ingredients:\n")
                };
        }

        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(jsonControl.JsonFileName))
            {

                //De - serialize to object or create new list
                IngredientsList = JsonConvert.DeserializeObject<List<Ingredient>>(jsonControl.GetJsonData());
                ItemsMenu = new List<EntityMenu>(ItemsMenuMain);

                foreach (var ingredient in IngredientsList.OrderBy(x=>x.Name))
                {
                    ItemsMenu.Add(new Ingredient() { Id=ingredient.Id, Name = $"    {ingredient.Name}" });
                }

            }
            else Console.WriteLine(" There are no ingredients! Add ingredients!");

            Console.WriteLine();
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
                        if(ItemsMenu[id].Id!=0)
                        _ = new ContextMenuCategories(new IngredientControl(), jsonControl.JsonFileName, ItemsMenu[id].Id, new List<EntityMenu>(IngredientsList));
                    }
                    break;
            }
        }

        protected override void AddMenuItem(string filePath)
        {
            try
            {
                Console.WriteLine();
                int id = IngredientsList.Max(x => x.Id) + 1;
                Console.Write(" Enter name ingredient: ");
                string nameIngredient = Console.ReadLine();

                do
                {
                    if (IngredientsList.Exists(x => x.Name.ToLower() == nameIngredient.ToLower().Trim()))
                    {
                        Console.Write(" An ingredient with that name already exists. Enter another name: ");
                        nameIngredient = Console.ReadLine();
                    }
                }
                while (IngredientsList.Exists(x => x.Name.ToLower() == nameIngredient.ToLower()));

                IngredientsList.Add(new Ingredient() { Id = id, Name = nameIngredient });
                // Update json data string
                File.WriteAllText(filePath, JsonConvert.SerializeObject(IngredientsList));
                ReturnPreviousMenu();
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }

        override public void ReturnPreviousMenu()
        {
            new IngredientControl().GetMenuItems();
        }
    }
}
