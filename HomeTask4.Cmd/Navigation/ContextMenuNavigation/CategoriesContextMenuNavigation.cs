using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class CategoriesContextMenuNavigation : NavigationManager, ICategoriesContextMenuNavigation
    {
        private readonly ICategoriesController _categoriesController;

        private int CategoryId { get; set; }

        public CategoriesContextMenuNavigation(IValidationNavigation validationNavigation, ICategoriesController categoriesController) : base(validationNavigation)
        {
            _categoriesController = categoriesController;
        }

        private async Task Rename()
        {
            Console.Write("    Enter new name: ");
            string newName = await ValidationNavigation.NullOrEmptyText(Console.ReadLine());
            await _categoriesController.RenameAsync(CategoryId, newName);
        }

        private async Task Delete()
        {
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if ((await ValidationNavigation.YesNoAsync()) == ConsoleKey.N)
            {
                return;
            }
            await _categoriesController.DeleteAsync(CategoryId);
        }
        public async Task ShowMenu(int categoryId)
        {
            CategoryId = categoryId;
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
