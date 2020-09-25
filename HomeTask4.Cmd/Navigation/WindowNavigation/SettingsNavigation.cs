using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class SettingsNavigation : BaseNavigation, INavigation
    {
        private readonly ISettingsController SettingsControl;
        public SettingsNavigation(IUnitOfWork unitOfWork, ISettingsController settingsControl) : base(unitOfWork)
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
                        CategoriesNavigation catNav = new CategoriesNavigation(UnitOfWork, new CategoriesController(UnitOfWork));
                        new ProgramMenu(catNav).CallMenu();
                    }
                    break;
                case 1:
                    {
                        IngredientsNavigation ingrNav = new IngredientsNavigation(UnitOfWork, new IngredientsControl(UnitOfWork));
                        new ProgramMenu(ingrNav).CallMenu();
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
                        MainWindowNavigation mainWinNav = new MainWindowNavigation(UnitOfWork);
                        new ProgramMenu(mainWinNav).CallMenu();
                    }
                    break;
            }
        }
    }
}
