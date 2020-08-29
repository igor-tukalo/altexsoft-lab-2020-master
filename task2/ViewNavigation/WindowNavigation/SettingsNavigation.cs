using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;

namespace task2.ViewNavigation.WindowNavigation
{
    class SettingsNavigation : INavigation
    {
        ISettingsControl SettingsControl;

        public SettingsNavigation(ISettingsControl settingsControl)
        {
            SettingsControl = settingsControl;
        }

        public void GetNavigation()
        {
            Console.Clear();
            List<EntityMenu> ItemsMenu = new List<EntityMenu>
            {
                 new EntityMenu() { Name = "    Customize сategories" },
                 new EntityMenu() { Name = "    Customize ingredients" },
                 new EntityMenu() { Name = "    Сhange the number of rows in the navigation menu" },
                 new EntityMenu() { Name = "    Return to main menu" }
            };

            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        new ProgramMenu(new CategoriesNavigation(new CategoriesControl())).CallMenu();
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new IngredientsNavigation(new IngredientsControl())).CallMenu();
                    }
                    break;
                case 2:
                    {
                        SettingsControl.EditBatch();
                        GetNavigation();
                    }
                    break;
                case 3:
                    {
                        new ProgramMenu(new MainWindowNavigation()).CallMenu();
                    }
                    break;
            }
        }
    }
}
