using HomeTask4.Cmd.Interfaces;
using HomeTask4.Cmd.Interfaces.ContextMenuNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class CookingStepsContextMenuNavigation : NavigationManager, ICookingStepsContextMenuNavigation
    {
        private readonly ICookingStepsController _cookingStepsController;
        private int _cookingStepId;

        public CookingStepsContextMenuNavigation(IConsoleHelper consoleHelper,
            ICookingStepsController cookingStepsController) : base(consoleHelper)
        {
            _cookingStepsController = cookingStepsController;
        }

        private async Task EditAsync(int cookingStepId)
        {
            CookingStep cookingStep = await _cookingStepsController.GetCookingStepByIdAsync(cookingStepId);
            Console.WriteLine($"\n    Describe the cooking step {cookingStep.Step}: ");
            string stepName = await ConsoleHelper.CheckNullOrEmptyTextAsync(Console.ReadLine());
            cookingStep.Name = stepName;
            await _cookingStepsController.EditAsync(cookingStep);
        }

        private async Task DeleteAsync(int cookingStepId)
        {
            Console.WriteLine("\n    Do you really want to remove the cooking step? ");
            if (await ConsoleHelper.ShowYesNoAsync() == ConsoleKey.N)
            {
                return;
            }
            await _cookingStepsController.DeleteAsync(cookingStepId);
        }

        public async Task ShowMenuAsync(int cookingStepId)
        {
            _cookingStepId = cookingStepId;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Edit" },
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
                        await EditAsync(_cookingStepId);
                    }
                    break;
                case 1:
                    {
                        await DeleteAsync(_cookingStepId);
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
