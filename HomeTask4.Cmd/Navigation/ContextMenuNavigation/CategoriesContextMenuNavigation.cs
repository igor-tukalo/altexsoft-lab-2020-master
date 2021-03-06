﻿using HomeTask4.Cmd.Interfaces;
using HomeTask4.Cmd.Interfaces.ContextMenuNavigation;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    public class CategoriesContextMenuNavigation : NavigationManager, ICategoriesContextMenuNavigation
    {
        private readonly ICategoriesController _categoriesController;
        private int _categoryId;

        public CategoriesContextMenuNavigation(IConsoleHelper consoleHelper, ICategoriesController categoriesController) : base(consoleHelper)
        {
            _categoriesController = categoriesController;
        }

        private async Task RenameAsync()
        {
            Console.Write("    Enter new name: ");
            string newName = await ConsoleHelper.CheckNullOrEmptyTextAsync(Console.ReadLine());
            int parentId = (int)(await _categoriesController.GetCategoryByIdAsync(_categoryId)).ParentId;
            await _categoriesController.EditCategoryAsync(_categoryId, newName, parentId);
        }

        private async Task DeleteAsync()
        {
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if ((await ConsoleHelper.ShowYesNoAsync()) == ConsoleKey.N)
            {
                return;
            }
            await _categoriesController.DeleteCategoryAsync(_categoryId);
        }
        public async Task ShowMenuAsync(int categoryId)
        {
            _categoryId = categoryId;
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
