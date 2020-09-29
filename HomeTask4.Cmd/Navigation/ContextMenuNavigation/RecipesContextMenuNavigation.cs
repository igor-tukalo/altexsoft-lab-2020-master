using HomeTask4.Cmd;
using HomeTask4.Cmd.Navigation;
using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    internal class RecipesContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        private readonly int IdRecipe;
        private int IdCategory { get; set; }

        private readonly IRecipesController Recipes;

        public RecipesContextMenuNavigation(IUnitOfWork unitOfWork, int idRecipe, IRecipesController recipes) : base(unitOfWork)
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
            IdCategory = UnitOfWork.Repository.GetById<Recipe>(IdRecipe).CategoryId;
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
                        RecipesIngredientsNavigation recipesIngredientsNavigation = new RecipesIngredientsNavigation(UnitOfWork,
                            IdRecipe, new IngredientsController(UnitOfWork), new RecipeIngredientsController(UnitOfWork));
                        new ProgramMenu(recipesIngredientsNavigation).CallMenu();
                    }
                    break;
                case 4:
                    {
                        CookingStepsNavigation cookingStepsNav = new CookingStepsNavigation(UnitOfWork, IdRecipe, new CookingStepsController(UnitOfWork));
                        new ProgramMenu(cookingStepsNav).CallMenu();
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
            RecipesNavigation recipeNav = new RecipesNavigation(UnitOfWork, Recipes);
            recipeNav.MovementCategoriesRecipes(IdCategory);
        }
    }
}
