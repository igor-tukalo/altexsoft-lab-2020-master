using HomeTask4.Cmd.Navigation.WindowNavigation;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation.ContextMenuNavigation
{
    internal class CookingStepsContextMenuNavigation : BaseNavigation, IContextMenuNavigation
    {
        private readonly ICookingStepsControl CookingSteps;
        private readonly int IdCookingStep;
        private readonly int IdRecipe;
        public CookingStepsContextMenuNavigation(int idCookingStep, int idRecipe, ICookingStepsControl cookingSteps)
        {
            IdCookingStep = idCookingStep;
            IdRecipe = idRecipe;
            CookingSteps = cookingSteps;
        }

        public override void CallNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
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
            CookingStepsNavigation CookStepNav = new CookingStepsNavigation(IdRecipe, CookingSteps);
            try
            {
                new ProgramMenu(CookStepNav).CallMenu();
            }
            finally
            {
                CookStepNav.Dispose();
            }
        }
    }
}
