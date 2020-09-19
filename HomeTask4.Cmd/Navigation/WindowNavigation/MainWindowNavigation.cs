﻿using HomeTask4.Core.CRUD;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class MainWindowNavigation : BaseNavigation, INavigation
    {
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
            ItemsMenu = new List<EntityMenu>
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
                        RecipesNavigation recipeNav = new RecipesNavigation(new CategoriesControl(UnitOfWork), new RecipesControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(recipeNav).CallMenu();
                        }
                        finally
                        {
                            recipeNav.Dispose();
                        }
                    }
                    break;
                case 1:
                    {
                        SettingsNavigation settNav = new SettingsNavigation(new SettingsControl(UnitOfWork));
                        try
                        {
                            new ProgramMenu(settNav).CallMenu();
                        }
                        finally
                        {
                            settNav.Dispose();
                        }
                    }
                    break;
            }
        }
    }
}
