using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class CategoriesContextMenuNavigation : INavigation
    {
        readonly int Idcategory;
        readonly ICategoriesControl Categories;
        
        public CategoriesContextMenuNavigation(int idCategory, ICategoriesControl categories)
        {
            Idcategory = idCategory;
            Categories = categories;
        }
        
        public void GetNavigation()
        {
            Console.Clear();
            List<EntityMenu> ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };

            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Categories.Rename(Idcategory);
                        new ProgramMenu(new CategoriesNavigation(new CategoriesControl())).CallMenu();
                    }
                    break;
                case 1:
                    {
                        Categories.Delete(Idcategory);
                        new ProgramMenu(new CategoriesNavigation(new CategoriesControl())).CallMenu();
                    }
                    break;
                case 2:
                    {
                        new ProgramMenu(new CategoriesNavigation(new CategoriesControl())).CallMenu();
                    }
                    break;
            }
        }
    }
}
