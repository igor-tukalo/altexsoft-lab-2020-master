using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class CookingStepsNavigation : NavigationManager, ICookingStepsNavigation
    {
        private readonly ICookingStepsController _cookingStepsController;
        private List<EntityMenu> itemsMenu;
        private int recipeId;

        public CookingStepsNavigation(IValidationNavigation validationNavigation, ICookingStepsController cookingStepsController) : base(validationNavigation)
        {
            _cookingStepsController = cookingStepsController;
        }

        private async Task<List<EntityMenu>> GetItemsAsync(List<EntityMenu> itemsMenu, int idRecipe)
        {
            List<CookingStep> cookingSteps = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(idRecipe);
            foreach (CookingStep s in cookingSteps)
            {
                if (itemsMenu != null)
                {
                    itemsMenu.Add(new EntityMenu() { Id = s.Id, Name = $"    {s.Step}. {s.Name}", ParentId = s.RecipeId });
                }
            }
            return itemsMenu;
        }

        private async Task AddAsync(int recipeId)
        {
            int currentStep = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId)).Any() ?
            (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId)).Max(x => x.Step) + 1 : 1;
            Console.Write($"\n    Describe the cooking step {currentStep}: ");
            string stepName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _cookingStepsController.Add(recipeId, currentStep, stepName);
            Console.Write("\n    Add another cooking step? ");
            if (await ValidationNavigation.YesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await AddAsync(recipeId);
        }

        public async Task ShowMenuAsync(int id)
        {
            recipeId = id;
            Console.Clear();
            itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Add step cooking" },
                    new EntityMenu(){ Name= "    Cancel" }
                };
            await GetItemsAsync(itemsMenu, id);
            await CallNavigationAsync(itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddAsync(recipeId);
                    }
                    break;
                case 1:
                    {

                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
    }
}
