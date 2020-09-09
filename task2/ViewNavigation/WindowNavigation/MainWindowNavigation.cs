using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class MainWindowNavigation : BaseNavigation, INavigation
    {
        readonly UnitOfWork unitOfWork = new UnitOfWork();
        public override void CallNavigation()
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
            base.ItemsMenu = new List<EntityMenu>
            {
                new EntityMenu() { Name = "    Recipes" },
                new EntityMenu() { Name = "    Settings" }
            };
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        new ProgramMenu(new RecipesNavigation(new CategoriesControl(unitOfWork),new RecipesControl(unitOfWork))).CallMenu();
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
