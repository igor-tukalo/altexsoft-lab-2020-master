using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class RecipesIngredientsNavigation : BaseNavigation, INavigation
    {
        readonly IRecipeIngredientsControl RecipeIngredients;
        readonly IIngredientsControl Ingredients;
        public int PageIngredients = 1;
        public RecipesIngredientsNavigation(IIngredientsControl ingredients, IRecipeIngredientsControl recipeIngredients)
        {
            Ingredients = ingredients;
            RecipeIngredients = recipeIngredients;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Go to page", TypeEntity="pages" },
                    new EntityMenu(){ Name= "    Сreate a new ingredient" },
                    new EntityMenu(){ Name= "    Cancel" },
                };
            ItemsMenu.Add(new EntityMenu() { Name = "\n    Recipe ingredients:\n" });
            ItemsMenu = RecipeIngredients.Get(ItemsMenu, RecipeIngredients.IdRecipe);
            ItemsMenu.Add(new EntityMenu() { Name = "\n    Ingredients to add:\n" });
            ItemsMenu = Ingredients.GetIngredientsBatch(ItemsMenu, PageIngredients);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Console.Write("\n    Enter page number: ");
                        PageIngredients = Validation.BatchExist(Console.ReadLine(), ItemsMenu[id].ParentId);
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        Ingredients.Add();
                        CallNavigation();
                    }
                    break;
                case 2:
                    {
                        new ProgramMenu(new RecipesContextMenuNavigation(RecipeIngredients.IdRecipe, new RecipesControl())).CallMenu();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].TypeEntity == "ingrRecipe")
                        {
                            RecipeIngredients.Delete(ItemsMenu[id].Id);
                            CallNavigation();
                        }
                        else if (ItemsMenu[id].TypeEntity == "ingr")
                        {
                            RecipeIngredients.Add(ItemsMenu[id].Id);
                            CallNavigation();
                        }
                    }
                    break;
            }
        }
    }
}
