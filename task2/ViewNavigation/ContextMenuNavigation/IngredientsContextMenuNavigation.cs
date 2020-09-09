using System;
using System.Collections.Generic;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class IngredientsContextMenuNavigation : BaseNavigation, IContextMenuNavigation
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

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Ingredients.Edit(IdIngredient);
                        BackPrevMenu();
                    }
                    break;
                case 1:
                    {
                        Ingredients.Delete(IdIngredient);
                        BackPrevMenu();
                    }
                    break;
                case 2:
                    {
                        BackPrevMenu();
                    }
                    break;
            }
        }

        public void BackPrevMenu()
        {
            IngredientsNavigation ingredientsNavigation = new IngredientsNavigation(Ingredients)
            {
                PageIngredients = Page
            };
            new ProgramMenu(ingredientsNavigation).CallMenu();
        }
    }
}
