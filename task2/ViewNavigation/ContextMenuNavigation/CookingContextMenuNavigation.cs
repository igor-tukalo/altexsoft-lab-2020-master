using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.WindowNavigation;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    class CookingStepsContextMenuNavigation : INavigation
    {
        ICookingStepsControl CookingSteps;
        readonly int IdCookingStep;

        public CookingStepsContextMenuNavigation(int idCookingStep, ICookingStepsControl cookingSteps)
        {
            IdCookingStep = idCookingStep;
            CookingSteps = cookingSteps;
        }

        public void GetNavigation()
        {
            Console.Clear();
            List<EntityMenu> ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name = "    Edit" },
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
                        CookingSteps.Edit(IdCookingStep);
                        new ProgramMenu(new CookingStepsNavigation(new CookingStepsControl(CookingSteps.IdRecipe))).CallMenu();
                    }
                    break;
                case 1:
                    {
                        CookingSteps.Delete(IdCookingStep, CookingSteps.IdRecipe);
                        new ProgramMenu(new CookingStepsNavigation(new CookingStepsControl(CookingSteps.IdRecipe))).CallMenu();
                    }
                    break;
                case 2:
                    {
                        new ProgramMenu(new CookingStepsNavigation(new CookingStepsControl(CookingSteps.IdRecipe))).CallMenu();
                    }
                    break;
            }
        }
    }
}
