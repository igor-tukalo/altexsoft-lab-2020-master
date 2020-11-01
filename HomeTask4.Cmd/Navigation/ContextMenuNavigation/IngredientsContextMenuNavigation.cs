using HomeTask4.Cmd.Interfaces;
using HomeTask4.Cmd.Interfaces.ContextMenuNavigation;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class IngredientsContextMenuNavigation : NavigationManager, IIngredientsContextMenuNavigation
    {
        private readonly IIngredientsController _ingredientsController;
        private int _ingredientId;

        public IngredientsContextMenuNavigation(IConsoleHelper consoleHelper,
            IIngredientsController ingredientsController) : base(consoleHelper)
        {
            _ingredientsController = ingredientsController;
        }
        private async Task RenameAsync()
        {
            Console.Write("    Enter new name: ");
            string newName = await ConsoleHelper.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _ingredientsController.RenameAsync(_ingredientId, newName);
        }

        private async Task DeleteAsync()
        {
            Console.WriteLine("    Do you really want to remove the ingredient? ");
            if ((await ConsoleHelper.ShowYesNoAsync()) == ConsoleKey.N)
            {
                return;
            }
            await _ingredientsController.DeleteAsync(_ingredientId);
        }

        public async Task ShowMenuAsync(int ingredientId)
        {
            _ingredientId = ingredientId;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
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
                        await RenameAsync();
                    }
                    break;
                case 1:
                    {
                        await DeleteAsync();
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
