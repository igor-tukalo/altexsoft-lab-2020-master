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
        protected List<Ingredient> AddedIngredients { get; set; } // list of ingredients that can be changed before adding
        protected Recipe RecipeEntity { get; set; }

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
        virtual public void GetMenuIngredientsChangeBeforeAdding(Recipe addedRecipe, EntityMenu category, List<Ingredient> addedIngredients = null, List<EntityMenu> itemsMenuEntity=null)
        {
            CategoryRecipe = category;
            AddedIngredients = addedIngredients;
            RecipeEntity = addedRecipe;

            Console.Clear();

            if (itemsMenuEntity is null)
                ItemsMenu = new List<EntityMenu>
            {
                new Category(name: "    Go to the steps of preparing the recipe"),
                new Category(name: "    Remove previously added ingredient"),
                new Category(name: "    Cancel create recipe")
            };
            else ItemsMenu = itemsMenuEntity;

            foreach (var ingr in Ingredients.OrderBy(x => x.Name))
            {
                if(AddedIngredients ==null || !AddedIngredients.Exists(x=>x.Id==ingr.Id))
                ItemsMenu.Add(new Ingredient(id: ingr.Id, name: ingr.Name));
            }

            ViewAddedIngredients(RecipeEntity.Id);

            Console.WriteLine("\nTo add an ingredient, select it from the list by pressing Enter.");
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
                                recipeAddStepsCookingControl.GetMenuItems(CategoryRecipe, RecipeEntity, AmountRecipeIngredients);
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
                                //Ingredients.Add(AddedIngredients[AddedIngredients.Count - 1]);
                                AddedIngredients.RemoveAt(AddedIngredients.Count - 1);
                                AmountRecipeIngredients.RemoveAt(AmountRecipeIngredients.Count - 1);

                                ReturnPreviousMenu();
                            }
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
                        AddIngredientToRecipe(ItemsMenu[id].Id, RecipeEntity.Id);
                    }
                    break;
            }
        }

        protected void AddIngredientToRecipe(int idIngredient, int idRecipe)
        {
            int idAmount = AmountRecipeIngredients.Max(x => x.Id) + 1;

            Console.Write("\n Enter the amount of ingredient: ");
            int amount = Validation.ValidNumber(Console.ReadLine());

            Console.Write(" Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());

            var amountRecipeIngredient = new AmountRecipeIngredient() { Id = idAmount, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = idRecipe };
            AmountRecipeIngredients.Add(amountRecipeIngredient);

            var ingredient = (from r in Ingredients
                              where r.Id == idIngredient
                              select r).First();

            AddedIngredients ??= new List<Ingredient>();

            AddedIngredients.Add(ingredient);

            // remove the ingredient to prevent duplicate ingredients
            Ingredients.Remove(ingredient);
            ReturnPreviousMenu();

        }

        virtual protected void ReturnPreviousMenu()
        {
            RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl(Ingredients, AmountRecipeIngredients);
            ingredientsRecipeControl.GetMenuIngredientsChangeBeforeAdding(RecipeEntity, CategoryRecipe, AddedIngredients);
        }

        protected void ViewAddedIngredients(int idRecipe)
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

        virtual protected void Cancel()
        {
            NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
            navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
        }
    }
}
