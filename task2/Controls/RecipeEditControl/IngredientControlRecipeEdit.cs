using System;
using System.Collections.Generic;
using System.Text;
using task2.Controls.RecipeAddConrols;
using task2.Models;

namespace task2.Controls.RecipeEditControl
{
    class IngredientControlRecipeEdit : IngredientControlRecipeAdd
    {
        public IngredientControlRecipeEdit(Recipe recipeEdit, EntityMenu category, List<AmountRecipeIngredient> amountRecipeIngredients) : base(recipeEdit, category, amountRecipeIngredients)
        {
        }

        protected override void ReturnEditRecipe()
        {
            RecipeEditIngredientsControl recipeEditIngredientsControl = new RecipeEditIngredientsControl(IngredientsList, AmountRecipeIngredients);
            recipeEditIngredientsControl.GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe);
        }
    }
}
