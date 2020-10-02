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
        private int IngredientId { get; set; }

        public IngredientsContextMenuNavigation(IValidationNavigation validationNavigation,
            IIngredientsController ingredientsController) : base(validationNavigation)
        {
            _ingredientsController = ingredientsController;
        }
        private async Task Rename()
        {
            Console.Write("    Enter new name: ");
            string newName = await ValidationNavigation.NullOrEmptyText(Console.ReadLine());
            await _ingredientsController.RenameAsync(IngredientId, newName);
        }

        private async Task Delete()
        {
            Console.WriteLine("    Do you really want to remove the ingredient? ");
            if ((await ValidationNavigation.YesNoAsync()) == ConsoleKey.N)
            {
                return;
            }
            await _ingredientsController.DeleteAsync(IngredientId);
        }

        public async Task ShowMenu(int id)
        {
            IngredientId = id;
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
            await CallNavigation(itemsMenu, SelectMethodMenu);
        }

        public async Task SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await Rename();
                    }
                    break;
                case 1:
                    {
                        await Delete();
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
