using HomeTask4.Cmd.Navigation.ContextMenuNavigation;
using HomeTask4.Core.Controllers;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using task2.ViewNavigation.ContextMenuNavigation;

namespace HomeTask4.Cmd.Navigation.WindowNavigation
{
    internal class CookingStepsNavigation : BaseNavigation, INavigation
    {
        private readonly ICookingStepsController CookingSteps;
        private readonly int IdRecipe;

        public CookingStepsNavigation(IUnitOfWork unitOfWork, int idRecipe, ICookingStepsController cookingSteps) : base(unitOfWork)
        {
            IdRecipe = idRecipe;
            CookingSteps = cookingSteps;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Add step cooking" },
                    new EntityMenu(){ Name= "    Cancel" }
                };
            CookingSteps.GetItems(ItemsMenu, IdRecipe);
            base.CallNavigation();
        }

        public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        CookingSteps.Add(IdRecipe);
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        RecipesContextMenuNavigation recipeNav = new RecipesContextMenuNavigation(UnitOfWork, IdRecipe, new RecipesController(UnitOfWork));
                        new ProgramMenu(recipeNav).CallMenu();
                    }
                    break;
                default:
                    {
                        CookingStepsContextMenuNavigation cookStepsContextNav = new CookingStepsContextMenuNavigation(UnitOfWork, ItemsMenu[id].Id, IdRecipe, CookingSteps);
                        new ProgramMenu(cookStepsContextNav).CallMenu();
                    }
                    break;
            }
        }
    }
}
