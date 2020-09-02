using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class SettingsNavigation : BaseNavigation, INavigation
    {
        readonly ISettingsControl SettingsControl;

        public SettingsNavigation(ISettingsControl settingsControl)
        {
            SettingsControl = settingsControl;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
            {
                 new EntityMenu() { Name = "    Customize сategories" },
                 new EntityMenu() { Name = "    Customize ingredients" },
                 new EntityMenu() { Name = "    Сhange the number of rows in the navigation menu" },
                 new EntityMenu() { Name = "    Return to main menu" }
            };
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
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
                        CallNavigation();
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
