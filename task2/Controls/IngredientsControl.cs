using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class IngredientsControl : MenuNavigation
    {
        public IngredientsControl()
        {
            unitOfWork = new UnitOfWork();

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

            ItemsMenu = new List<EntityMenu>(ItemsMenuMain);

            foreach (var ingredient in unitOfWork.Ingredients.GetAll().OrderBy(x => x.Name))
            {
                ItemsMenu.Add(new Ingredient() { Id = ingredient.Id, Name = $"    {ingredient.Name}" });
            }
            Console.WriteLine("\n  Ingredients list");
            Console.WriteLine("\n  Select an ingredient from the list and press ENTER to control.");
            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        AddMenuItem();
                        GetMenuItems();
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
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
                            _ = new ContextMenuIngredients(unitOfWork, ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        protected override void AddMenuItem()
        {
            try
            {
                Console.WriteLine();
                int id = unitOfWork.Ingredients.GetAll().Max(x => x.Id) + 1;

                Console.Write(" Enter name ingredient: ");
                string nameIngredient = Validation.IsNameMustNotExist(new List<EntityMenu>(unitOfWork.Ingredients.GetAll()), Console.ReadLine());

                unitOfWork.Ingredients.Create(new Ingredient(id, nameIngredient));
                unitOfWork.SaveDataTable("Ingredients.json", JsonConvert.SerializeObject(unitOfWork.Ingredients.GetAll()));
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
