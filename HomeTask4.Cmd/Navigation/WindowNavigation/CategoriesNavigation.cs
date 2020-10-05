using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class CategoriesNavigation : NavigationManager, ICategoriesNavigation
    {
        private readonly ICategoriesController _categoriesController;
        private readonly ICategoriesContextMenuNavigation _categoriesContextMenuNavigation;
        private List<EntityMenu> _itemsMenu;

        public CategoriesNavigation(IValidationNavigation validationNavigation,
            ICategoriesController categoriesController,
            ICategoriesContextMenuNavigation categoriesContextMenuNavigation) : base(validationNavigation)
        {
            _categoriesController = categoriesController;
            _categoriesContextMenuNavigation = categoriesContextMenuNavigation;
        }
        private async Task AddCategoryAsync()
        {
            Console.Write("\n    Enter name category: ");
            string name = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            Console.Write("    Enter name main category: ");
            string parentСategoryName = await ValidationNavigation.CheckNullOrEmptyTextAsync(Console.ReadLine());
            await _categoriesController.AddAsync(name, parentСategoryName);
            await ShowMenuAsync();
        }

        private async Task BuildHierarchicalCategoriesAsync(List<EntityMenu> items, Category category, int level)
        {
            if (items != null && category != null)
            {
                items.Add(new EntityMenu() { Id = category.Id, Name = $"{new string('-', level)}{category.Name}", ParentId = category.ParentId });
            }
            List<Category> categoriesWhereParentId = await _categoriesController.GetItemsWhereParentIdAsync(category.Id);
            foreach (Category child in categoriesWhereParentId.OrderBy(x => x.Name))
            {
                await BuildHierarchicalCategoriesAsync(items, child, level + 1);
            }
        }
        private async Task ShowContextMenuAsync(int categoryId)
        {
            await _categoriesContextMenuNavigation.ShowMenuAsync(categoryId);
            await ShowMenuAsync();
        }

        public async Task ShowMenuAsync()
        {
            Console.Clear();
            _itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add category" },
                    new EntityMenu(){ Name = "    Return to settings"},
                };
            Category category = await _categoriesController.GetByIdAsync(1);
            await BuildHierarchicalCategoriesAsync(_itemsMenu, category, 1);
            await CallNavigationAsync(_itemsMenu, SelectMethodMenuAsync);
        }
        public async Task SelectMethodMenuAsync(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    {
                        await AddCategoryAsync();
                    }
                    break;
                case 1:
                    {
                    }
                    break;
                default:
                    {
                        if (_itemsMenu[menuId].ParentId != 0)
                        {
                            await ShowContextMenuAsync(_itemsMenu[menuId].Id);
                        }
                        else
                        {
                            await ShowMenuAsync();
                        }
                    }
                    break;
            }
        }
    }
}
