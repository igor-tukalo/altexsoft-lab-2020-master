using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;

namespace task2.ViewNavigation.WindowNavigation
{
    public class MainWindowNavigation : INavigation
    {
        public void GetNavigation()
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

            List<EntityMenu> ItemsMenu = new List<EntityMenu>
            {
                new EntityMenu() { Name = "    Recipes" },
                new EntityMenu() { Name = "    Settings" }
            };

            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        new ProgramMenu(new RecipesNavigation(new CategoriesControl(),new RecipesControl())).CallMenu();
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new SettingsNavigation(new SettingsControl())).CallMenu();
                    }
                    break;
            }
        }
    }
}
