using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeAddIngredientsControl : RecipeViewControl
    {
        
        public RecipeAddIngredientsControl()
        {
            ItemsMenuMain = new List<EntityMenu>
            {
                new Category(name: "    Add a new ingredient to the ingredient selection list"),
                new Category(name: "    Go to the steps of preparing the recipe"),
                new Category(name: "    Cancel create recipe")
            };
        }

        public RecipeAddIngredientsControl(List<Ingredient> ingredients, List<AmountRecipeIngredient> amountRecipeIngredients)
        {
            Ingredients = ingredients;
            AmountRecipeIngredients = amountRecipeIngredients;

            ItemsMenuMain = new List<EntityMenu>
            {
                new Category(name: "    Add a new ingredient to the ingredient selection list"),
                new Category(name: "    Go to the steps of preparing the recipe"),
                new Category(name: "    Cancel create recipe")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addedRecipe"></param>
        /// <param name="category">to return to the category menu when canceling adding an ingredient</param>
        virtual public void GetMenuIngredientsChangeBeforeAdding(Recipe addedRecipe, EntityMenu category)
        {
            CategoryRecipe = category;
            RecipeViewSelected = addedRecipe;

            Console.Clear();

            ViewAddedIngredients(RecipeViewSelected.Id);

            Console.WriteLine("\nTo add an ingredient, select it from the list by pressing Enter.");
            CallMenuNavigation();
        }

        protected void ViewAddedIngredients(int idRecipe)
        {
            ItemsMenu = new List<EntityMenu>(ItemsMenuMain)
            {
                new Ingredient(name: "\n    Added ingredients:\n")
            };

            foreach (var amount in AmountRecipeIngredients.Where(x => x.IdRecipe == idRecipe))
            {
                foreach (var ingr in Ingredients.Where(x => x.Id == amount.IdIngredient))
                {
                    ItemsMenu.Add(new Category(id: ingr.Id, name: $"    {ingr.Name} {amount.Amount} {amount.Unit}", typeEntity: "amountIngr"));
                }
            }

            ItemsMenu.Add(new Ingredient(name: "\n    Ingredients to add:\n"));

            foreach (var ingr in Ingredients.OrderBy(x => x.Name))
            {
                if (!ItemsMenu.Exists(x => x.Id == ingr.Id))
                    ItemsMenu.Add(new Category(id: ingr.Id, name: $"    {ingr.Name}", typeEntity: "ingr"));
            }
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Add a new ingredient to the ingredient selection list
                        if (ItemsMenu.Where(x => x.TypeEntity == "amountIngr").Count() > 0)
                        {
                            IngredientControlRecipeAdd ingredientControlRecipe = new IngredientControlRecipeAdd(RecipeViewSelected, CategoryRecipe, AmountRecipeIngredients);
                            ingredientControlRecipe.GetMenuItems();
                        }
                    }
                    break;
                case 1:
                    {
                        // Go to the steps of preparing the recipe
                        if (ItemsMenu.Where(x => x.TypeEntity == "amountIngr").Count() > 0)
                        {
                            RecipeAddStepsCookingControl recipeAddStepsCookingControl = new RecipeAddStepsCookingControl();
                            recipeAddStepsCookingControl.GetMenuItems(CategoryRecipe, RecipeViewSelected, AmountRecipeIngredients);
                        }
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

        protected void AddIngredientToRecipe(int idIngredient, int idRecipe)
        {
            int idAmount = AmountRecipeIngredients.Max(x => x.Id) + 1;

            Console.Write("\n Enter the amount of ingredient: ");
            double amount = Validation.ValidDouble(Console.ReadLine().Replace(".",","));

            Console.Write(" Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());

            var amountRecipeIngredient = new AmountRecipeIngredient() { Id = idAmount, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = idRecipe };
            AmountRecipeIngredients.Add(amountRecipeIngredient);
            ReturnPreviousMenu();
        }

        protected void AddOrDeleteIngredient(EntityMenu EntityItemMenu)
        {
            // Remove ingredient
            if (EntityItemMenu.TypeEntity == "amountIngr")
            {
                AmountRecipeIngredients.Remove(GetAmountIngredient(EntityItemMenu.Id));
                ReturnPreviousMenu();
            }// Add ingredient
            else if (EntityItemMenu.TypeEntity == "ingr")
            {
                AddIngredientToRecipe(EntityItemMenu.Id, RecipeViewSelected.Id);
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

        override public void ReturnPreviousMenu()
        {
            RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl(Ingredients, AmountRecipeIngredients);
            ingredientsRecipeControl.GetMenuIngredientsChangeBeforeAdding(RecipeViewSelected, CategoryRecipe);
        }

        virtual protected void Cancel()
        {
            NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
            navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
        }
    }
}
