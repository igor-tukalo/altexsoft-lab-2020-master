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

        public MainWindowNavigation(IValidationNavigation validationNavigation,
            ISettingsNavigation settingsNavigation) : base(validationNavigation)
        {
            _settingsNavigation = settingsNavigation;
        }

        private Task GotoRecipes()
        {
            return Task.CompletedTask;
        }

        private async Task GoToSettings()
        {
            Task task;
            do
            {
                task = _settingsNavigation.ShowMenu();
                await task;
            }
            while (!task.IsCompleted);
            await ShowMenu();
        }

        public async Task ShowMenu()
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
            await CallNavigation(itemsMenu, SelectMethodMenu);
        }

        public async Task SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        await GotoRecipes();
                    }
                    break;
                case 1:
                    {
                        await GoToSettings();
                    }
                    break;
            }
        }
    }
}
