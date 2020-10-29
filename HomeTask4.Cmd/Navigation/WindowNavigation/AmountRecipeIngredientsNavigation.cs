using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class AmountRecipeIngredientsNavigation : IngredientsNavigation, IAmountRecipeIngredientsNavigation
    {
        private readonly IAmountRecipeIngredientsController _amountRecipeIngredientsController;
        private int _recipeId;
        public AmountRecipeIngredientsNavigation(IConsoleHelper validationNavigation,
            IIngredientsController ingredientsController,
            IIngredientsContextMenuNavigation ingredientsContextMenuNavigation,
            IAmountRecipeIngredientsController amountRecipeIngredientsController) : base(validationNavigation, ingredientsController, ingredientsContextMenuNavigation)
        {
            _amountRecipeIngredientsController = amountRecipeIngredientsController;
        }

        private async Task<List<EntityMenu>> GetAmountRecipeIngredientsAsync(int recipeId, List<EntityMenu> entityMenu)
        {
            List<AmountIngredient> amountIngredients = await _amountRecipeIngredientsController.GetAmountIngredietsAsync(recipeId);
            foreach (AmountIngredient amountIngredient in amountIngredients)
            {
                string ingredientName = await _amountRecipeIngredientsController.GetAmountIngredientNameAsync(amountIngredient.IngredientId);
                entityMenu.Add(new EntityMenu() { Id = amountIngredient.Id, Name = $"    {ingredientName} - {amountIngredient.Amount} {amountIngredient.Unit}", TypeEntity = "addedIngr" });
            }
            return entityMenu;
        }

        private async Task AddAsync(int recipeId, int ingredientId)
        {
            try
            {
                Console.Write("\n    Enter the amount of ingredient: ");
                double amount = double.Parse(Console.ReadLine().Replace(".", ","));
                Console.Write("    Enter the unit of ingredient: ");
                string unit = await ConsoleHelper.CheckNullOrEmptyTextAsync(Console.ReadLine());
                await _amountRecipeIngredientsController.AddAsync(amount, unit, recipeId, ingredientId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("\n    Press any key...");
                Console.ReadKey();
            }
        }

        private async Task DeleteAsync(int amountIngredientId)
        {
            await _amountRecipeIngredientsController.DeleteAsync(amountIngredientId);
        }

        protected override async Task ShowContextMenuAsync(int menuId)
        {
            if (ItemsMenu[menuId].TypeEntity == "addedIngr")
            {
                await DeleteAsync(ItemsMenu[menuId].Id);
            }
            else if (ItemsMenu[menuId].TypeEntity == "ingr")
            {
                await AddAsync(_recipeId, ItemsMenu[menuId].Id);
            }
        }

        public override async Task SelectMethodMenuAsync(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    {
                        await AddIngredientAsync();
                        await ShowMenuAsync(_recipeId);
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                case 2:
                    {
                        await GoToPageAsync();
                        await ShowMenuAsync(_recipeId);
                    }
                    break;
                default:
                    {
                        await ShowContextMenuAsync(menuId);
                        await ShowMenuAsync(_recipeId);
                    }
                    break;
            }
        }

        public async Task ShowMenuAsync(int recipeId)
        {
            _recipeId = recipeId;
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
            {
                new EntityMenu() { Name = "    Add ingredient" },
                new EntityMenu() { Name = "    Return to the previous menu" },
                new EntityMenu() { Name = "    Go to page", TypeEntity = "pages" },
                new EntityMenu() { Name = "\n    Recipe ingredients:\n" }
            };
            ItemsMenu = await GetAmountRecipeIngredientsAsync(recipeId, ItemsMenu);
            await ShowMenuAsync();
        }

        public override async Task ShowMenuAsync()
        {
            ItemsMenu.Add(new EntityMenu() { Name = "\n    Ingredients to add:\n" });
            ItemsMenu = await GetIngredientsBatchAsync(ItemsMenu, PageIngredients);
            await CallNavigationAsync(ItemsMenu, SelectMethodMenuAsync);
        }
    }
}
