using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel;
using System;
using System.Collections.Generic;
using task2.ViewNavigation.ContextMenuNavigation;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class RecipesIngredientsNavigation : BaseNavigation, INavigation
    {
        private readonly Validation ValidManager = new Validation();
        private readonly IRecipeIngredientsControl RecipeIngredients;
        private readonly IIngredientsControl Ingredients;
        public int PageIngredients = 1;
        private readonly int IdRecipe;
        public RecipesIngredientsNavigation(int idRecipe, IIngredientsControl ingredients, IRecipeIngredientsControl recipeIngredients)
        {
            IdRecipe = idRecipe;
            Ingredients = ingredients;
            RecipeIngredients = recipeIngredients;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Go to page", TypeEntity="pages" },
                    new EntityMenu(){ Name= "    Сreate a new ingredient" },
                    new EntityMenu(){ Name= "    Cancel" },
                };
            ItemsMenu.Add(new EntityMenu() { Name = "\n    Recipe ingredients:\n" });
            ItemsMenu = RecipeIngredients.GetItems(ItemsMenu, IdRecipe);
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
                        PageIngredients = ValidManager.BatchExist(Console.ReadLine(), ItemsMenu[id].ParentId);
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
                        RecipesContextMenuNavigation recipeContextMenuNav = new RecipesContextMenuNavigation(IdRecipe, new RecipesController(UnitOfWork));
                        new ProgramMenu(recipeContextMenuNav).CallMenu();
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
                            RecipeIngredients.Add(ItemsMenu[id].Id, IdRecipe);
                            CallNavigation();
                        }
                    }
                    break;
            }
        }
    }
}
