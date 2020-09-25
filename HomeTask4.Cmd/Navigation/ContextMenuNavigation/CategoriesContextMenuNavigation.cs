using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    internal class CategoriesContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        private readonly int Idcategory;
        private readonly ICategoriesController Categories;

        public CategoriesContextMenuNavigation(IUnitOfWork unitOfWork, int idCategory, ICategoriesController categories) : base(unitOfWork)
        {
            Idcategory = idCategory;
            Categories = categories;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Categories.Edit(Idcategory);
                        BackPrevMenu();
                    }
                    break;
                case 1:
                    {
                        Categories.Delete(Idcategory);
                        BackPrevMenu();
                    }
                    break;
                case 2:
                    {
                        BackPrevMenu();
                    }
                    break;
            }
        }

        public void BackPrevMenu()
        {
            CategoriesNavigation catNav = new CategoriesNavigation(UnitOfWork, Categories);
            new ProgramMenu(catNav).CallMenu();
        }
    }
}
