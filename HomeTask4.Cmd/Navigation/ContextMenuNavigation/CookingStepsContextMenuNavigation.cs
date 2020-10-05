using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class CookingStepsContextMenuNavigation : NavigationManager, ICookingStepsContextMenuNavigation
    {
        private readonly ICookingStepsController _cookingStepsController;
        private int cookingStepId;

        public CookingStepsContextMenuNavigation(IValidationNavigation validationNavigation,
            ICookingStepsController cookingStepsController) : base(validationNavigation)
        {
            _cookingStepsController = cookingStepsController;
        }

        private async Task EditAsync(int id)
        {
            CookingStep cookingStep = await _cookingStepsController.GetCookingStepByIdAsync(id);
            Console.WriteLine($"\n    Describe the cooking step {cookingStep.Step}: ");
            string stepName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            cookingStep.Name = stepName;
            await _cookingStepsController.EditAsync(cookingStep);
        }

        private async Task DeleteAsync(int id)
        {
            Console.WriteLine("\n    Do you really want to remove the cooking step? ");
            if (await ValidationNavigation.YesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await _cookingStepsController.DeleteAsync(id);
        }

        public async Task ShowMenuAsync(int id)
        {
            cookingStepId = id;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Edit" },
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
                        await EditAsync(cookingStepId);
                    }
                    break;
                case 1:
                    {
                        await DeleteAsync(cookingStepId);
                    }
                    break;
                case 2:
                    {

                    }
                    break;
            }
        }
    }
}
