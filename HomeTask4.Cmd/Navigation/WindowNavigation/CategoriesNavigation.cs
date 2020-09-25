using HomeTask4.Cmd.Navigation.ContextMenuNavigation;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class CategoriesNavigation : BaseNavigation, INavigation
    {
        private readonly ICategoriesController Categories;
        public CategoriesNavigation(IUnitOfWork unitOfWork, ICategoriesController categories) : base(unitOfWork)
        {
            Categories = categories;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add category" },
                    new EntityMenu(){ Name = "    Return to settings"},
                    new EntityMenu(){ Name = "    Return to main menu"}
                };
            Categories.BuildHierarchicalCategories(ItemsMenu, UnitOfWork.Repository.GetById<Category>(1), 1);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Categories.Add();
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        SettingsNavigation settNav = new SettingsNavigation(UnitOfWork, new SettingsController(UnitOfWork));
                        new ProgramMenu(settNav).CallMenu();
                    }
                    break;
                case 2:
                    {
                        MainWindowNavigation mainWnNav = new MainWindowNavigation(UnitOfWork);
                        new ProgramMenu(mainWnNav).CallMenu();
                    }
                    break;
                default:
                    {
                        CategoriesContextMenuNavigation catContextNav = new CategoriesContextMenuNavigation(UnitOfWork, ItemsMenu[id].Id, Categories);
                        new ProgramMenu(catContextNav).CallMenu();
                    }
                    break;
            }
        }
    }
}
