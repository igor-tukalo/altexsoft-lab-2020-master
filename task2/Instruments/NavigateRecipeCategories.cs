using task2.Controls;
using task2.Models;

namespace task2.Instruments
{
    public class NavigateRecipeCategories : RecipesCategoryControl
    {
        public delegate void Method(int id);
        public void GetRecipesCategory(EntityMenu entityMenu, int IdPrevCategory)
        {
            // forward movement. one levels below
            if (entityMenu.Id != IdPrevCategory)
                GetMenuItems(entityMenu.Id);
            // backward movement. one level up
            else
                GetMenuItems(entityMenu.ParentId);
        }
    }
}