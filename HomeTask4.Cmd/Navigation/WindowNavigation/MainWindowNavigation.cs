using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    public class MainWindowNavigation : NavigationManager, IMainWindowNavigation
    {
        private readonly ISettingsNavigation _settingsNavigation;
        private readonly IRecipesNavigation _recipesNavigation;

        public MainWindowNavigation(IValidationNavigation validationNavigation,
            ISettingsNavigation settingsNavigation, IRecipesNavigation recipesNavigation) : base(validationNavigation)
        {
            _settingsNavigation = settingsNavigation;
            _recipesNavigation = recipesNavigation;
        }

        private async Task GotoRecipesAsync()
        {
            await _recipesNavigation.ShowMenuAsync();
            await ShowMenuAsync();
        }

        private async Task GoToSettingsAsync()
        {
            await _settingsNavigation.ShowMenuAsync();
            await ShowMenuAsync();
        }

        public async Task ShowMenuAsync()
        {
            Console.Clear();
            Console.WriteLine(@"
                 _.--._  _.--._
            ,-=.-':;:;:;\':;:;:;'-._
            \\\:;:;:;:;:;\:;:;:;:;:;\
             \\\:;:;:;:;:;\:;:;:;:;:;\
              \\\:;:;:;:;:;\:;:;:;:;:;\
               \\\:;:;:;:;:;\:;::;:;:;:\
                \\\;:;::;:;:;\:;:;:;::;:\
                 \\\;;:;:_:--:\:_:--:_;:;\    Welcome to the Cook Book!
                  \\\_.-'      :      ''-.\
                   \`_..--''--.;.--'''--.._\
                    ");
            List<EntityMenu> itemsMenu = new List<EntityMenu>
            {
                new EntityMenu() { Name = "    Recipes" },
                new EntityMenu() { Name = "    Settings" }
            };
            await CallNavigationAsync(itemsMenu, SelectMethodMenuAsync);
        }

        public async Task SelectMethodMenuAsync(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await GotoRecipesAsync();
                    }
                    break;
                case 1:
                    {
                        await GoToSettingsAsync();
                    }
                    break;
            }
        }
    }
}
