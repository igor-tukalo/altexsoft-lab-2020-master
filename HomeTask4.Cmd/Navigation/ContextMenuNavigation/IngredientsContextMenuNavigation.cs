using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class IngredientsContextMenuNavigation : NavigationManager, IIngredientsContextMenuNavigation
    {
        private readonly IIngredientsController _ingredientsController;
        private int ingredientId;

        public IngredientsContextMenuNavigation(IValidationNavigation validationNavigation,
            IIngredientsController ingredientsController) : base(validationNavigation)
        {
            _ingredientsController = ingredientsController;
        }
        private async Task RenameAsync()
        {
            Console.Write("    Enter new name: ");
            string newName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _ingredientsController.RenameAsync(ingredientId, newName);
        }

        private async Task DeleteAsync()
        {
            Console.WriteLine("    Do you really want to remove the ingredient? ");
            if ((await ValidationNavigation.YesNoAsync()) == ConsoleKey.N)
            {
                return;
            }
            await _ingredientsController.DeleteAsync(ingredientId);
        }

        public async Task ShowMenuAsync(int id)
        {
            ingredientId = id;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
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
