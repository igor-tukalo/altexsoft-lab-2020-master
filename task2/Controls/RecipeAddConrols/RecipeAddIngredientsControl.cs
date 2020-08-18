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
        List<Ingredient> AddedIngredients { get; set; }
        Recipe AddedRecipe { get; set; }

        public RecipeAddIngredientsControl()
        {
        }

        public RecipeAddIngredientsControl(List<Ingredient> ingredients, List<AmountRecipeIngredient> amountRecipeIngredients)
        {
            Ingredients = ingredients;
            AmountRecipeIngredients = amountRecipeIngredients;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRecipe"></param>
        /// <param name="category">to return to the category menu when canceling adding an ingredient</param>
        /// <param name="addedIngredients"></param>
        public void GetMenuAddIngredientToRecipe(Recipe addedRecipe, EntityMenu category, List<Ingredient> addedIngredients = null)
        {
            CategoryRecipe = category;
            AddedIngredients = addedIngredients;
            AddedRecipe = addedRecipe;

            Console.Clear();

            ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "    Go to the steps of preparing the recipe"),
                new Category(name: "    Remove previously added ingredient"),
                new Category(name: "    Cancel create recipe")
            };

            foreach (var ingr in Ingredients.OrderBy(x=>x.Name))
            {
                ItemsMenu.Add(new Ingredient(id: ingr.Id, name: ingr.Name));
            }

            ViewAddedIngredients(AddedRecipe.Id);

            Console.WriteLine("\n To add an ingredient, select it from the list by pressing Enter.\n");
            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Go to the steps of preparing the recipe
                        if (AddedIngredients != null)
                        {
                            if (AddedIngredients.Count > 0)
                            {
                                RecipeAddStepsCookingControl recipeAddStepsCookingControl = new RecipeAddStepsCookingControl();
                                recipeAddStepsCookingControl.GetMenuItems(CategoryRecipe, AddedRecipe, AmountRecipeIngredients);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        // Remove previously added ingredient
                        if (AddedIngredients != null)
                        {
                            if (AddedIngredients.Count > 0)
                            {
                                Ingredients.Add(AddedIngredients[AddedIngredients.Count - 1]);
                                AddedIngredients.RemoveAt(AddedIngredients.Count - 1);
                                AmountRecipeIngredients.RemoveAt(AmountRecipeIngredients.Count - 1);

                                RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl(Ingredients, AmountRecipeIngredients);
                                ingredientsRecipeControl.GetMenuAddIngredientToRecipe(AddedRecipe, CategoryRecipe, AddedIngredients);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        // Cancel. Back to recipe categories
                        NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                        navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
                    }
                    break;
                default:
                    {
                        AddIngredientToRecipe(ItemsMenu[id].Id);
                    }
                    break;
            }
        }

        private void AddIngredientToRecipe(int idIngredient)
        {
            int idAmount = AmountRecipeIngredients.Max(x => x.Id) + 1;

            Console.Write("\n Enter the amount of ingredient: ");
            int amount = Validation.ValidNumber(Console.ReadLine());

            Console.Write(" Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());

            var amountRecipeIngredient = new AmountRecipeIngredient() { Id = idAmount, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = AddedRecipe.Id };
            AmountRecipeIngredients.Add(amountRecipeIngredient);

            var ingredient = (from r in Ingredients
                              where r.Id == idIngredient
                              select r).First();

            AddedIngredients ??= new List<Ingredient>();

            AddedIngredients.Add(ingredient);

            // remove the ingredient to prevent duplicate ingredients
            Ingredients.Remove(ingredient);

            RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl(Ingredients, AmountRecipeIngredients);
            ingredientsRecipeControl.GetMenuAddIngredientToRecipe(AddedRecipe, CategoryRecipe, AddedIngredients);
        }

        private void ViewAddedIngredients(int idRecipe)
        {
            Console.Write(" Added ingredients: ");

            List<string> addedIngredients = new List<string>();
            foreach (var amount in AmountRecipeIngredients.Where(x => x.IdRecipe == idRecipe))
            {
                var ingredient = (from r in AddedIngredients
                                  where r.Id == amount.IdIngredient
                                  select r).FirstOrDefault();
                if (ingredient != null)
                    addedIngredients.Add($"{ingredient.Name} {amount.Amount} {amount.Unit}");
            }
            Console.WriteLine(string.Join(", ", addedIngredients));
        }
    }
}
