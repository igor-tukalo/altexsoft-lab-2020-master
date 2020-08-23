using System.Collections.Generic;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    class СreateIngredientWhenChangeRecipeControl : IngredientsControl
    {
        public СreateIngredientWhenChangeRecipeControl(UnitOfWork _unitOfWork, int recipeId)
        {
            unitOfWork = _unitOfWork;
            RecipeId = recipeId;

            ItemsMenuMain = new List<EntityMenu>
                {
                    new Ingredient(name: "  Add ingredient"),
                    new Ingredient(name: "  Return to edit ingredients list"),
                    new Ingredient(name: "\n  Ingredients:\n")
                };
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        AddMenuItem();
                        GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        new RecipeIngredientsControl(unitOfWork, RecipeId).GetMenuItems();
                    }
                    break;
                // when selecting categories, we call the context menu of actions
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
                            _ = new ContextMenuIngredientWhenChangeRecipe(unitOfWork, ItemsMenu[id].Id, RecipeId);
                    }
                    break;
            }
        }
    }
}
