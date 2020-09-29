﻿using HomeTask4.Cmd;
using HomeTask4.Cmd.Navigation;
using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    internal class IngredientsContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        private readonly IIngredientsController Ingredients;
        private readonly int IdIngredient;
        private readonly int Page;

        public IngredientsContextMenuNavigation(IUnitOfWork unitOfWork, int idIngredient, int page, IIngredientsController ingredients) : base(unitOfWork)
        {
            Ingredients = ingredients;
            IdIngredient = idIngredient;
            Page = page;
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
                        Ingredients.Edit(IdIngredient);
                        BackPrevMenu();
                    }
                    break;
                case 1:
                    {
                        Ingredients.Delete(IdIngredient);
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
            IngredientsNavigation ingredientsNavigation = new IngredientsNavigation(UnitOfWork, Ingredients)
            {
                PageIngredients = Page
            };
            new ProgramMenu(ingredientsNavigation).CallMenu();
        }
    }
}