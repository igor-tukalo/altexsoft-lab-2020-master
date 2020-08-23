using System;
using System.Collections.Generic;
using task2.Models;
using task2.Repositories;

namespace task2.Instruments
{
    public abstract class ContextMenuNavigation : MenuNavigation
    {
        protected int IdMenuNavigation { get; set; }

        /// <summary>
        /// Call the action menu for the specified item in the navigation menu
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="idMenuNavigation"></param>
        public ContextMenuNavigation(UnitOfWork _unitOfWork,  int idMenuNavigation, int recipeId = 0)
        {
            RecipeId = recipeId;
            Console.Clear();

            unitOfWork = _unitOfWork;
            IdMenuNavigation = idMenuNavigation;

            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "  Rename"),
                new Category(name: "  Delete"),
                new Category(name: "  Cancel")
            };

            CallMenuNavigation();
        }

        protected virtual void Rename() { }
        protected virtual void Delete() { }
        protected virtual void Cancel() { }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0: { Rename(); } break;
                case 1: { Delete(); } break;
                case 2: { Cancel(); } break;
            }
        }


    }
}
