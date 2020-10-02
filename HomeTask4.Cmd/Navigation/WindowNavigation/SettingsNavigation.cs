using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class SettingsNavigation : NavigationManager, ISettingsNavigation
    {
        private readonly ICategoriesNavigation _categoriesNavigation;
        private readonly IIngredientsNavigation _ingredientsNavigation;

        public SettingsNavigation(IValidationNavigation validationNavigation,
            IIngredientsNavigation ingredientsNavigation,
            ICategoriesNavigation categoriesNavigation) : base(validationNavigation)
        {
            _ingredientsNavigation = ingredientsNavigation;
            _categoriesNavigation = categoriesNavigation;
        }

        private async Task CustomizeCategories()
        {
            Task task;
            do
            {
                task = _categoriesNavigation.ShowMenu();
                await task;
            }
            while (!task.IsCompleted);
            await ShowMenu();
        }

        private async Task CustomizeIngredients()
        {
            Task task;
            do
            {
                task = _ingredientsNavigation.ShowMenu();
                await task;
            }
            while (!task.IsCompleted);
            await ShowMenu();
        }

        public async Task ShowMenu()
        {
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
            {
                 new EntityMenu() { Name = "    Customize сategories" },
                 new EntityMenu() { Name = "    Customize ingredients" },
                 new EntityMenu() { Name = "    Return to main menu" }
            };
            await CallNavigation(itemsMenu, SelectMethodMenu);
        }

        public async Task SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await CustomizeCategories();
                    }
                    break;
                case 1:
                    {
                        await CustomizeIngredients();
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
