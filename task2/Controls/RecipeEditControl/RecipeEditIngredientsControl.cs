using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Controls.RecipeEditControl;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeEditIngredientsControl : RecipeAddIngredientsControl
    {
        public RecipeEditIngredientsControl()
        {
            ItemsMenuMain = new List<EntityMenu>
                {
                    new Category(name: "    Add a new ingredient to the ingredient selection list"),
                    new Category(name: "    Save ingredient changes"),
                    new Category(name: "    Cancel. Back to recipe categories")
                };
        }

        public RecipeEditIngredientsControl(List<Ingredient> ingredients, List<AmountRecipeIngredient> amountRecipeIngredients)
        {
            Ingredients = ingredients;
            AmountRecipeIngredients = amountRecipeIngredients;

            ItemsMenuMain = new List<EntityMenu>
                {
                    new Category(name: "    Add a new ingredient to the ingredient selection list"),
                    new Category(name: "    Save ingredient changes"),
                    new Category(name: "    Cancel. Back to recipe categories")
                };
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Add ingredient
                        IngredientControlRecipeEdit ingredientControlRecipe = new IngredientControlRecipeEdit(RecipeViewSelected,CategoryRecipe, AmountRecipeIngredients);
                        ingredientControlRecipe.GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        // Save ingredient changes
                        Validation.SaveSelectedDataJson(amountRecipeIngredients: AmountRecipeIngredients);
                        Cancel();
                    }
                    break;
                case 2:
                    {
                        // Cancel. Back to recipe categories
                        Cancel();
                    }
                    break;
                default:
                    {
                        AddOrDeleteIngredient(ItemsMenu[id]);
                    }
                    break;
            }
        }

        override public void ReturnPreviousMenu()
        {
            GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe);
        }

        protected override void Cancel()
        {
            // Cancel
            var recipe = (from r in Recipes
                          where r.Id == RecipeViewSelected.Id
                          select r).First();

            RecipeEditControl recipeEditControl = new RecipeEditControl();
            recipeEditControl.GetMenuItems(CategoryRecipe, recipe);
        }
    }
}
