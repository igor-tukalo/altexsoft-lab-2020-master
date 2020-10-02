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
        //private readonly ICategoriesContextMenuNavigation _categoriesContextMenuNavigation;
        private List<EntityMenu> ItemsMenu { get; set; }

        public CategoriesNavigation(IValidationNavigation validationNavigation,
            ICategoriesController categoriesController
            //,
            //ICategoriesContextMenuNavigation categoriesContextMenuNavigation
            ) : base(validationNavigation)
        {
            _categoriesController = categoriesController;
            //_categoriesContextMenuNavigation = categoriesContextMenuNavigation;
        }
        private async Task AddCategory()
        {
            Console.Write("\n    Enter name category: ");
            string name = await ValidationNavigation.NullOrEmptyText(Console.ReadLine());
            Console.Write("    Enter name main category: ");
            string parentСategoryName = await ValidationNavigation.NullOrEmptyText(Console.ReadLine());
            await _categoriesController.AddAsync(name, parentСategoryName);
            await ShowMenu();
        }

        private async Task BuildHierarchicalCategories(List<EntityMenu> items, Category category, int level)
        {
            if (items != null && category != null)
            {
                items.Add(new EntityMenu() { Id = category.Id, Name = $"{new string('-', level)}{category.Name}", ParentId = category.ParentId });
            }
            var categoriesWhereParentId = await _categoriesController.GetItemsWhereParentIdAsync(category.Id);
            foreach (Category child in categoriesWhereParentId.OrderBy(x => x.Name))
            {
                await BuildHierarchicalCategories(items, child, level + 1);
            }
        }
        private async Task CallContextMenuItem(int id)
        {
            //Task task;
            //do
            //{
            //    task = _categoriesContextMenuNavigation.ShowMenu(ItemsMenu[id].Id);
            //    await task;
            //}
            //while (!task.IsCompleted);
            await ShowMenu();
        }

        public async Task ShowMenu()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add category" },
                    new EntityMenu(){ Name = "    Return to settings"},
                };
            var category = await _categoriesController.GetByIdAsync(1);
            await BuildHierarchicalCategories(ItemsMenu, category, 1);
            await CallNavigation(ItemsMenu, SelectMethodMenu);
        }
        public async Task SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await AddCategory();
                    }
                    break;
                case 1:
                    {
                    }
                    break;
                default:
                    {
                        await CallContextMenuItem(id);
                    }
                    break;
            }
        }
    }
}
