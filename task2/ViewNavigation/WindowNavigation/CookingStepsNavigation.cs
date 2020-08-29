using System;
using System.Collections.Generic;
using task2.Controls;
using task2.Interfaces;
using task2.Models;
using task2.ViewNavigation.ContextMenuNavigation;

namespace task2.ViewNavigation.WindowNavigation
{
    class CookingStepsNavigation : INavigation
    {
        ICookingStepsControl CookingSteps;
        List<EntityMenu> ItemsMenu;

        public CookingStepsNavigation(ICookingStepsControl cookingSteps)
        {
            CookingSteps = cookingSteps;
        }

        public void GetNavigation()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new EntityMenu(){ Name= "    Add step cooking" },
                    new EntityMenu(){ Name= "    Cancel" }
                };

            CookingSteps.Get(ItemsMenu,CookingSteps.IdRecipe);
            new Navigation().GetNavigation(ItemsMenu, SelectMethodMenu);
        }

        void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        CookingSteps.Add(CookingSteps.IdRecipe);
                        GetNavigation();
                    }
                    break;
                case 1:
                    {
                        new ProgramMenu(new RecipesContextMenuNavigation(CookingSteps.IdRecipe, new RecipesControl())).CallMenu();
                    }
                    break;
                default:
                    {
                        new ProgramMenu(new CookingStepsContextMenuNavigation(ItemsMenu[id].Id, new CookingStepsControl(CookingSteps.IdRecipe))).CallMenu();
                    }
                    break;
            }
        }
    }
}
