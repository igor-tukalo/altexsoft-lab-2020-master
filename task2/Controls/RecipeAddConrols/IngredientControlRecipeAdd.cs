using System;
using System.Collections.Generic;
using System.Text;
using task2.Controls.RecipeAddConrols;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class IngredientControlRecipeAdd : IngredientControl
    {
        protected Recipe RecipeViewSelected { get; set; }
        protected List<AmountRecipeIngredient> AmountRecipeIngredients { get; set; }
        public IngredientControlRecipeAdd(Recipe recipeEdit, EntityMenu category, List<AmountRecipeIngredient> amountRecipeIngredients)
        {
            AmountRecipeIngredients = amountRecipeIngredients;
            RecipeViewSelected = recipeEdit;
            CategoryRecipe = category;
            jsonControl = new JsonControl("Ingredients.json");
            ItemsMenuMain = new List<EntityMenu>
                {
                    new Ingredient(name: "  Add ingredient"),
                    new Ingredient(name: "  Return to edit recipe"),
                    new Ingredient(name: "\n  Ingredients:\n")
                };
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        AddMenuItem(jsonControl.GetJsonPathFile());
                    }
                    break;
                case 1:
                    {
                        ReturnEditRecipe();
                    }
                    break;
                // when selecting categories, we call the context menu of actions
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
                            _ = new ContextMenuCategories(new IngredientControlRecipeAdd(RecipeViewSelected, CategoryRecipe, AmountRecipeIngredients), jsonControl.JsonFileName, ItemsMenu[id].Id, new List<EntityMenu>(IngredientsList));
                    }
                    break;
            }
        }

        virtual protected void ReturnEditRecipe()
        {
            RecipeAddIngredientsControl recipeAddIngredientsControl = new RecipeAddIngredientsControl(IngredientsList, AmountRecipeIngredients);
            recipeAddIngredientsControl.GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe);
        }

        override public void ReturnPreviousMenu()
        {
            new IngredientControlRecipeAdd(RecipeViewSelected,CategoryRecipe, AmountRecipeIngredients).GetMenuItems();
        }
    }
}
