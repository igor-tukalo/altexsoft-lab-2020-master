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

        public SettingsNavigation(IConsoleHelper validationNavigation,
            IIngredientsNavigation ingredientsNavigation,
            ICategoriesNavigation categoriesNavigation) : base(validationNavigation)
        {
            _ingredientsNavigation = ingredientsNavigation;
            _categoriesNavigation = categoriesNavigation;
        }

        private async Task CustomizeCategoriesAsync()
        {
            await _categoriesNavigation.ShowMenuAsync();
            await ShowMenuAsync();
        }

        private async Task CustomizeIngredientsAsync()
        {
            await _ingredientsNavigation.ShowMenuAsync();
            await ShowMenuAsync();
        }

        public async Task ShowMenuAsync()
        {
            Console.Clear();
            List<EntityMenu> itemsMenu = new List<EntityMenu>
            {
                 new EntityMenu() { Name = "    Customize сategories" },
                 new EntityMenu() { Name = "    Customize ingredients" },
                 new EntityMenu() { Name = "    Return to main menu" }
            };
            await CallNavigationAsync(itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int menuId)
        {
            switch (menuId)
            {
                case 0:
                    {
                        await CustomizeCategoriesAsync();
                    }
                    break;
                case 1:
                    {
                        await CustomizeIngredientsAsync();
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
