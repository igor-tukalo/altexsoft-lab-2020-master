using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class IngredientsNavigation : INavigation
    {
        IIngredientsControl Ingredients;

        public IngredientsNavigation(IIngredientsControl ingredients)
        {
            Ingredients = ingredients;
        }
        List<EntityMenu> ItemsMenu;
        public int PageIngredients = 1;
        public void GetNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add ingredient" },
                    new EntityMenu(){ Name = "    Return to settings"},
                    new EntityMenu(){ Name = "    Go to page", TypeEntity="pages"},
                    new EntityMenu(){ Name = "\n    Ingredients:\n" }
                };
            ItemsMenu = Ingredients.GetIngredientsBatch(ItemsMenu, PageIngredients);
            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Ingredients.Add();
                        GetNavigation();
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new SettingsNavigation(new SettingsControl())).CallMenu();
                    }
                    break;
                case 2:
                    {
                        Console.Write("\n    Enter page number: ");
                        PageIngredients = Validation.BatchExist(Console.ReadLine(), ItemsMenu[id].ParentId);
                        GetNavigation();
                    }
                    break;
                default:
                    {
                        new ProgramMenu(new IngredientsContextMenuNavigation(ItemsMenu[id].Id,PageIngredients,new IngredientsControl())).CallMenu();
                    }
                    break;
            }
        }

    }
}
