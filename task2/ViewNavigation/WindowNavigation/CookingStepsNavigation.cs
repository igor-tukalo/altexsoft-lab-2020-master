using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class CookingStepsNavigation : BaseNavigation, INavigation
    {
        readonly ICookingStepsControl CookingSteps;

        public CookingStepsNavigation(ICookingStepsControl cookingSteps)
        {
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
            CookingSteps.Get(ItemsMenu,CookingSteps.IdRecipe);
            base.CallNavigation();
        }

       public override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        CookingSteps.Add();
                        CallNavigation();
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new RecipesContextMenuNavigation(CookingSteps.IdRecipe, new RecipesControl(CookingSteps.UnitOfWork))).CallMenu();
                    }
                    break;
                default:
                    {
                        new ProgramMenu(new CookingStepsContextMenuNavigation(ItemsMenu[id].Id, new CookingStepsControl(CookingSteps.IdRecipe, CookingSteps.UnitOfWork))).CallMenu();
                    }
                    break;
            }
        }
    }
}
