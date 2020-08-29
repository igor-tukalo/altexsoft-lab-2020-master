using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class IngredientsContextMenuNavigation : INavigation
    {
        readonly IIngredientsControl Ingredients;
        readonly int IdIngredient;
        readonly int Page;
        public IngredientsContextMenuNavigation(int idIngredient, int page, IIngredientsControl ingredients)
        {
            Ingredients = ingredients;
            IdIngredient = idIngredient;
            Page = page;
        }
        public void GetNavigation()
        {
            Console.Clear();
            List<EntityMenu> ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };

            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Ingredients.Rename(IdIngredient);
                        IngredientsNavigation ingredientsNavigation = new IngredientsNavigation(new IngredientsControl())
                        {
                            PageIngredients = Page
                        };
                        new ProgramMenu(ingredientsNavigation).CallMenu();
                    }
                    break;
                case 1:
                    {
                        Ingredients.Delete(IdIngredient);
                        IngredientsNavigation ingredientsNavigation = new IngredientsNavigation(new IngredientsControl())
                        {
                            PageIngredients = Page
                        };
                        new ProgramMenu(ingredientsNavigation).CallMenu();
                    }
                    break;
                case 2:
                    {
                        IngredientsNavigation ingredientsNavigation = new IngredientsNavigation(new IngredientsControl())
                        {
                            PageIngredients = Page
                        };
                        new ProgramMenu(ingredientsNavigation).CallMenu();
                    }
                    break;
            }
        }
    }
}
