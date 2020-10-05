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
        private readonly ICookingStepsNavigation _cookingStepsNavigation;
        private int recipeId;

        public RecipesContextMenuNavigation(IValidationNavigation validationNavigation,
            IRecipesController recipesController,
            ICookingStepsNavigation cookingStepsNavigation) : base(validationNavigation)
        {
            _recipesController = recipesController;
            _cookingStepsNavigation = cookingStepsNavigation;
        }

        private async Task OpenRecipeAync(int id)
        {
            Console.Clear();
            Recipe recipe = await _recipesController.GetRecipeByIdAsync(id);
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    { await ValidationNavigation.WrapTextAsync(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");
            List<string> ingredients = await _recipesController.GetIngredientsWhereRecipeIdAsync(id);
            //ingredients recipe
            foreach (string ingredient in ingredients)
            {
                Console.WriteLine($"    {ingredient}");
            }
            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            List<CookingStep> cookingSteps = await _recipesController.GetCookingStepsWhereRecipeIdAsync(id);
            foreach (CookingStep s in cookingSteps.OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {await ValidationNavigation.WrapTextAsync(10, s.Name, "\n       ")}");
            }
            Console.WriteLine("\n    Press any key to return...");
            Console.ReadKey();
        }

        private async Task RenameRecipeAsync(int id)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string newName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _recipesController.RenameAsync(id, newName);
        }

        private async Task ChangeDescRecipe(int id)
        {
            Console.Write("\n    Enter recipe description: ");
            string desc = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _recipesController.ChangeDescription(id, desc);
        }

        private async Task ChangeIngredientsRecipeAsync(int id)
        {

            await OpenRecipeAync(id);
        }

        private async Task ChangeCookingStepsRecipeAsync(int id)
        {
            await _cookingStepsNavigation.ShowMenuAsync(id);
            await OpenRecipeAync(recipeId);
        }

        private async Task DeleteRecipeAsync(int id)
        {
            Console.Clear();
            Console.WriteLine("\n    Are you sure you want to delete the recipe? ");
            if (await ValidationNavigation.YesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await _recipesController.DeleteAsync(id);
        }

        public async Task ShowMenuAsync(int id)
        {
            recipeId = id;
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

        public async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await OpenRecipeAync(recipeId);
                    }
                    break;
                case 1:
                    {
                        await RenameRecipeAsync(recipeId);
                    }
                    break;
                case 2:
                    {
                        await ChangeDescRecipe(recipeId);
                    }
                    break;
                case 3:
                    {
                        await ChangeIngredientsRecipeAsync(recipeId);
                    }
                    break;
                case 4:
                    {
                        await ChangeCookingStepsRecipeAsync(recipeId);
                    }
                    break;
                case 5:
                    {
                        await DeleteRecipeAsync(recipeId);
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
