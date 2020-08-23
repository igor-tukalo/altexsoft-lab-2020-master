using task2.Controls;
using task2.Repositories;

namespace task2.Instruments
{
    public class ContextMenuIngredientWhenChangeRecipe : ContextMenuIngredients
    {
        public ContextMenuIngredientWhenChangeRecipe(UnitOfWork _unitOfWork, int idMenuNavigation, int recipeId = 0) : base(_unitOfWork, idMenuNavigation, recipeId)
        {
        }

        protected override void Cancel()
        {
            new СreateIngredientWhenChangeRecipeControl(unitOfWork, RecipeId).GetMenuItems();
        }
    }
}
