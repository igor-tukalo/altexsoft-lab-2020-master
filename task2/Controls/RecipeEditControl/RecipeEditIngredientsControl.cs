using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeEditIngredientsControl : RecipeAddIngredientsControl
    {
        public RecipeEditIngredientsControl()
        {
        }

        public void GetMenuEditIngredients(EntityMenu categoryRecipe, Recipe editableRecipe)
        {
            CategoryRecipe = categoryRecipe;
            RecipeViewSelected = editableRecipe;

            Console.Clear();

            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "    Add ingredient"),
                new Category(name: "    Save ingredient changes"),
                new Category(name: "    Cancel")
            };

            ItemsMenu.Add(new Ingredient(name: "\n    Added ingredients:\n"));
            foreach (var amount in AmountRecipeIngredients.Where(x => x.IdRecipe == RecipeViewSelected.Id))
            {
                foreach (var ingr in Ingredients.Where(x => x.Id == amount.IdIngredient))
                {
                    ItemsMenu.Add(new Category(id: ingr.Id, name: $"    {ingr.Name} {amount.Amount} {amount.Unit}", typeEntity:"amountIngr" ));
                    //AddedIngredients
                }
            }



            ItemsMenu.Add(new Ingredient(name: "\n    Ingredients to add:\n"));

            foreach (var ingr in Ingredients.OrderBy(x => x.Name))
            {
                if (!ItemsMenu.Exists(x => x.Id == ingr.Id))
                    ItemsMenu.Add(new Category(id: ingr.Id, name: $"    {ingr.Name}", typeEntity: "ingr"));
            }

            Console.WriteLine("\n    Edit recipe ingredients");
            Console.WriteLine("\n    Select and press Enter to remove or add an ingredient.");
            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Add ingredient
                        if (AmountRecipeIngredients.Where(x => x.IdRecipe == RecipeViewSelected.Id).Count() > 0)
                        {

                            RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl();
                            ingredientsRecipeControl.GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe, GetIngredientRecipe(RecipeViewSelected.Id));
                        }
                    }
                    break;
                case 1:
                    {
                        // Save ingredient changes
                        Validation.SaveSelectedDataJson(amountRecipeIngredients: AmountRecipeIngredients);
                        GetMenuEditIngredients(CategoryRecipe, RecipeViewSelected);
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
                        // Remove or add ingredient
                        if (ItemsMenu[id].TypeEntity == "amountIngr")
                        {
                            AmountRecipeIngredients.Remove(GetAmountIngredient(ItemsMenu[id].Id));
                            GetMenuEditIngredients(CategoryRecipe, RecipeViewSelected);
                        }
                        else if (ItemsMenu[id].TypeEntity == "ingr")
                        {
                            AddIngredientToRecipe(ItemsMenu[id].Id, RecipeViewSelected.Id);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Get the amount ingredients units for the specified recipe
        /// </summary>
        /// <param name="idRecipe"></param>
        /// <param name="etityIngredients"></param>
        /// <returns></returns>
        private List<Ingredient> GetIngredientRecipe(int idRecipe)
        {
            List<Ingredient> etityIngredients = new List<Ingredient>();
            foreach (var amount in AmountRecipeIngredients.Where(x => x.IdRecipe == idRecipe))
            {
                foreach (var ingr in Ingredients.Where(x => x.Id == amount.IdIngredient))
                {
                    etityIngredients.Add(new Ingredient(id: ingr.Id, name: $"{ingr.Name}"));
                }
            }
            return etityIngredients;
        }

        /// <summary>
        /// Get the amount ingredients units for the specified ingredient
        /// </summary>
        /// <param name="IdIngredient"></param>
        /// <returns></returns>
        private AmountRecipeIngredient GetAmountIngredient(int IdIngredient)
        {
            return (from a in AmountRecipeIngredients
                    where a.IdIngredient == IdIngredient
                    select a).First();
        }

        protected override void ReturnPreviousMenu()
        {
            GetMenuEditIngredients(CategoryRecipe, RecipeViewSelected);
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
