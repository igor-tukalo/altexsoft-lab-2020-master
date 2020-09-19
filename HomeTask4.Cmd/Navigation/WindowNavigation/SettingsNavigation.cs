﻿using HomeTask4.Core.CRUD;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class SettingsNavigation : BaseNavigation, INavigation
    {
        private readonly ISettingsControl SettingsControl;

        public SettingsNavigation(ISettingsControl settingsControl)
        {
            SettingsControl = settingsControl;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
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
                        CategoriesNavigation catNav = new CategoriesNavigation(new CategoriesControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(catNav).CallMenu();
                        }
                        finally
                        {
                            catNav.Dispose();
                        }
                    }
                    break;
                case 1:
                    {
                        IngredientsNavigation ingrNav = new IngredientsNavigation(new IngredientsControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(ingrNav).CallMenu();
                        }
                        finally
                        {
                            ingrNav.Dispose();
                        }
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
                        MainWindowNavigation mainWinNav = new MainWindowNavigation();
                        try
                        {
                            new ProgramMenu(mainWinNav).CallMenu();
                        }
                        finally
                        {
                            mainWinNav.Dispose();
                        }
                    }
                    break;
            }
        }
    }
}
