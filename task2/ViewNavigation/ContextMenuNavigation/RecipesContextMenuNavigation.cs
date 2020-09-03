using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class RecipesContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        readonly int IdRecipe;
        int IdCategory { get; set; }
        readonly IRecipesControl Recipes;
        
        public RecipesContextMenuNavigation(int idRecipe, IRecipesControl recipes)
        {
            IdRecipe = idRecipe;
            Recipes = recipes;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Open" },
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Change description" },
                    new EntityMenu(){ Name = "    Сhange ingredients list" },
                    new EntityMenu(){ Name = "    Сhange cooking steps" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            IdCategory = Recipes.GetIdCategory(IdRecipe);
            switch (id)
            {
                case 0:
                    {
                        Recipes.View(IdRecipe);
                    }
                    break;
                case 1:
                    {
                        Recipes.Edit(IdRecipe);
                        BackPrevMenu();
                    }
                    break;
                case 2:
                    {
                        Recipes.ChangeDescription(IdRecipe);
                        BackPrevMenu();
                    }
                    break;
                case 3:
                    {
                        new ProgramMenu(new RecipesIngredientsNavigation(new IngredientsControl(Recipes.UnitOfWork), new RecipeIngredientsControl(IdRecipe, Recipes.UnitOfWork))).CallMenu();
                    }
                    break;
                case 4:
                    {
                        new ProgramMenu(new CookingStepsNavigation(new CookingStepsControl(IdRecipe, Recipes.UnitOfWork))).CallMenu();
                    }
                    break;
                case 5:
                    {
                        Recipes.Delete(IdRecipe);
                        BackPrevMenu();
                    }
                    break;
                case 6:
                    {
                        BackPrevMenu();
                    }
                    break;
            }
        }

        public void BackPrevMenu()
        {
            new RecipesNavigation(new CategoriesControl(Recipes.UnitOfWork), new RecipesControl(Recipes.UnitOfWork)).MovementCategoriesRecipes(IdCategory);
        }
    }
}
