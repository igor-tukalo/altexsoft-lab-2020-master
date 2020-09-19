﻿using HomeTask4.Cmd;
using HomeTask4.Cmd.Navigation;
using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.CRUD;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    internal class RecipesContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        private readonly int IdRecipe;
        private int IdCategory { get; set; }

        private readonly IRecipesControl Recipes;

        public RecipesContextMenuNavigation(int idRecipe, IRecipesControl recipes)
        {
            IdRecipe = idRecipe;
            Recipes = recipes;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
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
                        RecipesIngredientsNavigation recipesIngredientsNavigation = new RecipesIngredientsNavigation(
                            IdRecipe, new IngredientsControl(UnitOfWork), new RecipeIngredientsControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(recipesIngredientsNavigation).CallMenu();
                        }
                        finally
                        {
                            recipesIngredientsNavigation.Dispose();
                        }
                    }
                    break;
                case 4:
                    {
                        CookingStepsNavigation cookingStepsNav = new CookingStepsNavigation(IdRecipe, new CookingStepsControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(cookingStepsNav).CallMenu();
                        }
                        finally
                        {
                            cookingStepsNav.Dispose();
                        }
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
            RecipesNavigation recipeNav = new RecipesNavigation(new CategoriesControl(UnitOfWork), Recipes);
            try
            {
                recipeNav.MovementCategoriesRecipes(IdCategory);
            }
            finally
            {
                recipeNav.Dispose();
            }
        }
    }
}
