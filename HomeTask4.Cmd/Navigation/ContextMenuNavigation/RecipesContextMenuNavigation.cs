using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class RecipesContextMenuNavigation : NavigationManager, IRecipesContextMenuNavigation
    {
        private readonly IRecipesController _recipesController;
        private readonly IAmountRecipeIngredientsNavigation _amountRecipeIngredientsNavigation;
        private readonly ICookingStepsNavigation _cookingStepsNavigation;
        private int _recipeId;

        public RecipesContextMenuNavigation(IValidationNavigation validationNavigation,
            IRecipesController recipesController,
            IAmountRecipeIngredientsNavigation amountRecipeIngredientsNavigation,
            ICookingStepsNavigation cookingStepsNavigation) : base(validationNavigation)
        {
            _recipesController = recipesController;
            _amountRecipeIngredientsNavigation = amountRecipeIngredientsNavigation;
            _cookingStepsNavigation = cookingStepsNavigation;
        }

        #region private methods
        private async Task OpenRecipeAync(int recipeId)
        {
            Console.Clear();
            Recipe recipe = await _recipesController.GetRecipeByIdAsync(recipeId);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    { await ValidationNavigation.WrapTextAsync(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            List<string> ingredients = await _recipesController.GetIngredientsWhereRecipeIdAsync(recipeId);
            //ingredients recipe
            foreach (string ingredient in ingredients)
            {
                Console.WriteLine($"    {ingredient}");
            }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            List<CookingStep> cookingSteps = await _recipesController.GetCookingStepsWhereRecipeIdAsync(recipeId);
            foreach (CookingStep s in cookingSteps.OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {await ValidationNavigation.WrapTextAsync(10, s.Name, "\n       ")}");
            }
            Console.WriteLine("\n    Press any key to return...");
            Console.ReadKey();
        }

        private async Task RenameRecipeAsync(int recipeId)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string newName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _recipesController.RenameAsync(recipeId, newName);
        }

        private async Task ChangeDescRecipe(int recipeId)
        {
            Console.Write("\n    Enter recipe description: ");
            string desc = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _recipesController.ChangeDescription(recipeId, desc);
        }

        private async Task ChangeIngredientsRecipeAsync(int recipeId)
        {
            await _amountRecipeIngredientsNavigation.ShowMenuAsync(recipeId);
            await ShowMenuAsync(recipeId);
        }

        private async Task ChangeCookingStepsRecipeAsync(int recipeId)
        {
            await _cookingStepsNavigation.ShowMenuAsync(recipeId);
            await ShowMenuAsync(recipeId);
        }

        private async Task DeleteRecipeAsync(int recipeId)
        {
            Console.Clear();
            Console.WriteLine("\n    Are you sure you want to delete the recipe? ");
            if (await ValidationNavigation.YesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await _recipesController.DeleteAsync(recipeId);
        } 
        #endregion

        public async Task ShowMenuAsync(int menuId)
        {
            _recipeId = menuId;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Open" },
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Change description" },
                    new EntityMenu(){ Name = "    Сhange ingredients list" },
                    new EntityMenu(){ Name = "    Сhange cooking steps" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
            await CallNavigationAsync(itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    {
                        await OpenRecipeAync(_recipeId);
                    }
                    break;
                case 1:
                    {
                        await RenameRecipeAsync(_recipeId);
                    }
                    break;
                case 2:
                    {
                        await ChangeDescRecipe(_recipeId);
                    }
                    break;
                case 3:
                    {
                        await ChangeIngredientsRecipeAsync(_recipeId);
                    }
                    break;
                case 4:
                    {
                        await ChangeCookingStepsRecipeAsync(_recipeId);
                    }
                    break;
                case 5:
                    {
                        await DeleteRecipeAsync(_recipeId);
                    }
                    break;
                case 6:
                    {
                    }
                    break;
            }
        }

    }
}
