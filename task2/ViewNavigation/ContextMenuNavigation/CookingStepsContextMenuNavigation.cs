using System;
using System.Collections.Generic;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class CookingStepsContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        readonly ICookingStepsControl CookingSteps;
        readonly int IdCookingStep;
        readonly int IdRecipe;
        public CookingStepsContextMenuNavigation(int idCookingStep, int idRecipe, ICookingStepsControl cookingSteps)
        {
            IdCookingStep = idCookingStep;
            IdRecipe = idRecipe;
            CookingSteps = cookingSteps;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            base.ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Edit" },
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
                        CookingSteps.Edit(IdCookingStep);
                        BackPrevMenu();
                    }
                    break;
                case 1:
                    {
                        CookingSteps.Delete(IdCookingStep, IdRecipe);
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
            new ProgramMenu(new CookingStepsNavigation(IdRecipe, CookingSteps)).CallMenu();
        }
    }
}
