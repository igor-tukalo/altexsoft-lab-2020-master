using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Models;

namespace task2.Instruments
{
    public class NavigateRecipeCategories : RecipeCategoriesControl
    {
        public delegate void Method(int id);
        public void GetRecipesCategory(EntityMenu entityMenu, int IdPrevCategory)
        {
            // cannot go one level higher than the parent
            //if (entityMenu.ParentId != 0)
                // forward movement. one levels below
                if (entityMenu.Id != IdPrevCategory)
                    GetMenuItems(entityMenu.Id);
                // backward movement. one level up
                else
                    GetMenuItems(entityMenu.ParentId);
        }
    }
}