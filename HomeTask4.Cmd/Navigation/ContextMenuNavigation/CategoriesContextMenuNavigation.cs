using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Interfaces.Navigation;
using HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    class CategoriesContextMenuNavigation : CategoriesNavigation, ICategoriesContextMenuNavigation
    {
        private int CategoryId { get; set; }

        public CategoriesContextMenuNavigation(ICategoriesController categoriesController) : base(categoriesController)
        {
        }
        public void ShowMenu(int categoryId)
        {
            if (categoryId == 0)
                throw new Exception("Invalid categoryId");
            else
            {
                CategoryId = categoryId;
                Console.Clear();
                var itemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Rename" },
                    new EntityMenu(){ Name = "    Delete"},
                    new EntityMenu(){ Name = "    Cancel"}
                };
                CallNavigation(itemsMenu, SelectMethodMenu);
            }
        }
        public void Rename()
        {
            Console.Write("    Enter new name: ");
            string newName = Console.ReadLine();
            categoriesController.RenameAsync(CategoryId, newName);
        }

        public void Delete()
        {
            if (categoriesController == null)
                throw new Exception("Fiasko");
             categoriesController.DeleteAsync(CategoryId);
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Rename();
                    }
                    break;
                case 1:
                    {
                        Delete();
                    }
                    break;
                case 2:
                    {

                    }
                    break;
            }
        }

    }
}
