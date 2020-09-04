using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class CategoriesContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        readonly int Idcategory;
        readonly ICategoriesControl Categories;

        public CategoriesContextMenuNavigation(int idCategory, ICategoriesControl categories)
        {
            Idcategory = idCategory;
            Categories = categories;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
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
            new ProgramMenu(new CategoriesNavigation(Categories)).CallMenu();
        }
    }
}
