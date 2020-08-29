using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class RecipesContextMenuNavigation : INavigation
    {
        readonly int IdRecipe;
        readonly IRecipesControl Recipes;
        
        public RecipesContextMenuNavigation(int idRecipe, IRecipesControl recipes)
        {
            IdRecipe = idRecipe;
            Recipes = recipes;
        }

        public void GetNavigation()
        {
            Console.Clear();
            List<EntityMenu> ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Open" },
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Change description" },
                    new EntityMenu(){ Name = "    Сhange ingredients list" },
                    new EntityMenu(){ Name = "    Сhange cooking steps" },
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
                        Recipes.View(IdRecipe);
                    }
                    break;
                case 1:
                    {
                        Recipes.Rename(IdRecipe);
                        new RecipesNavigation(new CategoriesControl(), new RecipesControl()).MovementCategoriesRecipes(Recipes.GetIdCategory(IdRecipe));
                    }
                    break;
                case 2:
                    {
                        Recipes.ChangeDescription(IdRecipe);
                        new RecipesNavigation(new CategoriesControl(), new RecipesControl()).MovementCategoriesRecipes(Recipes.GetIdCategory(IdRecipe));
                    }
                    break;
                case 3:
                    {
                        new ProgramMenu(new RecipesIngredientsNavigation(new IngredientsControl(), new RecipeIngredientsControl(IdRecipe))).CallMenu();
                    }
                    break;
                case 4:
                    {
                        new ProgramMenu(new CookingStepsNavigation(new CookingStepsControl(IdRecipe))).CallMenu();
                    }
                    break;
                case 5:
                    {
                        int idCategory = Recipes.GetIdCategory(IdRecipe);
                        Recipes.Delete(IdRecipe);
                        new RecipesNavigation(new CategoriesControl(), new RecipesControl()).MovementCategoriesRecipes(idCategory);
                    }
                    break;
                case 6:
                    {
                        new RecipesNavigation(new CategoriesControl(), new RecipesControl()).MovementCategoriesRecipes(Recipes.GetIdCategory(IdRecipe));
                    }
                    break;
            }
        }
    }
}
