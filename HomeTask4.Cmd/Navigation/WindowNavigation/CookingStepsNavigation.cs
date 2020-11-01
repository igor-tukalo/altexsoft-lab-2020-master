using HomeTask4.Cmd.Interfaces;
using HomeTask4.Cmd.Interfaces.ContextMenuNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class CookingStepsNavigation : NavigationManager, ICookingStepsNavigation
    {
        private readonly ICookingStepsController _cookingStepsController;
        private readonly ICookingStepsContextMenuNavigation _cookingStepsContextMenuNavigation;
        private List<EntityMenu> _itemsMenu;
        private int _recipeId;

        public CookingStepsNavigation(IConsoleHelper consoleHelper,
            ICookingStepsController cookingStepsController,
            ICookingStepsContextMenuNavigation cookingStepsContextMenuNavigation) : base(consoleHelper)
        {
            _cookingStepsController = cookingStepsController;
            _cookingStepsContextMenuNavigation = cookingStepsContextMenuNavigation;
        }

        private async Task<List<EntityMenu>> GetItemsAsync(List<EntityMenu> itemsMenu, int recipeId)
        {
            List<CookingStep> cookingSteps = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId);
            foreach (CookingStep cookingStep in cookingSteps)
            {
                if (itemsMenu != null)
                {
                    itemsMenu.Add(new EntityMenu() { Id = cookingStep.Id, Name = $"    {cookingStep.Step}. {cookingStep.Name}", ParentId = cookingStep.RecipeId });
                }
            }
            return itemsMenu;
        }

        private async Task AddAsync(int recipeId)
        {
            int currentStep = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId)).Any() ?
            (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId)).Max(x => x.Step) + 1 : 1;
            Console.WriteLine($"\n    Describe the cooking step {currentStep}: ");
            string stepName = await ConsoleHelper.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _cookingStepsController.AddAsync(recipeId, currentStep, stepName);
            Console.WriteLine("\n    Add another cooking step? ");
            if (await ConsoleHelper.ShowYesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await AddAsync(recipeId);
        }

        public async Task ShowMenuAsync(int recipeId)
        {
            _recipeId = recipeId;
            Console.Clear();
            _itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Add step cooking" },
                    new EntityMenu(){ Name= "    Cancel" }
                };
            await GetItemsAsync(_itemsMenu, _recipeId);
            await CallNavigationAsync(_itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    {
                        await AddAsync(_recipeId);
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                default:
                    {
                        await _cookingStepsContextMenuNavigation.ShowMenuAsync(_itemsMenu[menuId].Id);
                        await ShowMenuAsync(_recipeId);
                    }
                    break;
            }
        }
    }
}
