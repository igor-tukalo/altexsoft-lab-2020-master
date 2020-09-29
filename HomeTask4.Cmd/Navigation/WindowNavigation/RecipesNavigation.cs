﻿using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using task2.ViewNavigation.ContextMenuNavigation;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class RecipesNavigation : BaseNavigation, INavigation
    {
        private readonly IRecipesController Recipes;
        private int IdNextCategory = 1;
        private int IdPrevCategory;

        public RecipesNavigation(IUnitOfWork unitOfWork, IRecipesController recipes) : base(unitOfWork)
        {
            Recipes = recipes;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Add recipe" },
                    new EntityMenu(){ Name = "    Return to main menu" }
                };
            Category parent = UnitOfWork.Repository.GetById<Category>(IdNextCategory);
            IdPrevCategory = parent.ParentId;
            Recipes.BuildRecipesCategories(ItemsMenu, parent, 1, 2);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        Recipes.Add(IdNextCategory);
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        MainWindowNavigation mainWinNav = new MainWindowNavigation(UnitOfWork);
                        new ProgramMenu(mainWinNav).CallMenu();

                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].TypeEntity == "recipe")
                        {
                            RecipesContextMenuNavigation recipeContextManuNav = new RecipesContextMenuNavigation(UnitOfWork, ItemsMenu[id].Id, Recipes);
                            new ProgramMenu(recipeContextManuNav).CallMenu();
                        }
                        else
                        {
                            MovementCategoriesRecipes(ItemsMenu[id].Id);
                        }
                    }
                    break;
            }
        }

        public void MovementCategoriesRecipes(int idnextCategory)
        {
            // forward movement. one levels below
            if (IdNextCategory != idnextCategory)
            {
                IdNextCategory = idnextCategory;
            }
            // backward movement. one level up
            else
            {
                IdNextCategory = IdPrevCategory == 0 ? 1 : IdPrevCategory;
            }
            CallNavigation();
        }
    }
}