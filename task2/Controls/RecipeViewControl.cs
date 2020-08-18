using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using task2.Controls;
using task2.Controls.RecipeAddConrols;
using task2.Models;

namespace task2.Instruments
{
    public class RecipeViewControl : MenuNavigation
    {
        protected List<Recipe> Recipes { get; set; }
        protected List<AmountRecipeIngredient> AmountRecipeIngredients { get; set; }
        protected List<StepCooking> StepCookings { get; set; }
        protected List<Ingredient> Ingredients { get; set; }
        protected Recipe RecipeViewSelected { get; set; }

        public RecipeViewControl()
        {
            Recipes = JsonConvert.DeserializeObject<List<Recipe>>(new JsonControl("Recipes.json").GetJsonData());
            AmountRecipeIngredients = JsonConvert.DeserializeObject<List<AmountRecipeIngredient>>(new JsonControl("AmountsRecipeIngredients.json").GetJsonData());
            StepCookings = JsonConvert.DeserializeObject<List<StepCooking>>(new JsonControl("StepsCooking.json").GetJsonData());
            Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(new JsonControl("Ingredients.json").GetJsonData());
        }

        virtual public void GetMenuItems(EntityMenu categoryRecipe, Recipe recipe)
        {
            RecipeViewSelected = recipe;
            CategoryRecipe = categoryRecipe;
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Back to recipe category"),
                    new Category(name: "    Edit recipe"),
                    new Category(name: "    Delete recipe")
                };

            Console.WriteLine("\n    View recipe\n");
            RecipeView(RecipeViewSelected, AmountRecipeIngredients);
            CallMenuNavigation();
        }

        protected void RecipeView(Recipe recipe, List<AmountRecipeIngredient> amountRecipeIngredients=null)
        {
            if(amountRecipeIngredients == null)
            amountRecipeIngredients = AmountRecipeIngredients;
            //Console.Clear();
            Console.WriteLine($"{new string('\n', ItemsMenu.Count)}");
            Console.WriteLine($"{new string('\n', 5)}    ________{recipe.Name}________\n\n");
            Console.WriteLine($"    {Validation.WrapText(10, recipe.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");

            //ingredients recipe
            foreach (var a in amountRecipeIngredients.Where(x => x.IdRecipe == recipe.Id))
            {
                foreach (var i in Ingredients.Where(x => x.Id == a.IdIngredient))
                {
                    Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                }
            }

            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (var s in StepCookings.Where(x => x.IdRecipe == recipe.Id).OrderBy(x => x.Step))
            {
                Console.WriteLine($"    {s.Step}. {Validation.WrapText(10, s.Name, "\n       ")}");
            }
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Back to recipe category
                        NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                        navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
                    }
                    break;
                case 1:
                    {
                        // Edit recipe
                        RecipeEditControl recipeEditControl = new RecipeEditControl();
                        recipeEditControl.GetMenuItems(CategoryRecipe, RecipeViewSelected);
                    }
                    break;
                case 2:
                    {
                        // Delete recipe
                        Console.Clear();
                        Console.Write("Are you sure you want to delete the recipe? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            AmountRecipeIngredients.RemoveAll(i => i.IdRecipe == RecipeViewSelected.Id);
                            StepCookings.RemoveAll(i => i.IdRecipe == RecipeViewSelected.Id);
                            Recipes.RemoveAll(i => i.Id == RecipeViewSelected.Id);

                            // Update json data string
                            File.WriteAllText(new JsonControl("Recipes.json").GetJsonPathFile(), JsonConvert.SerializeObject(Recipes));
                            File.WriteAllText(new JsonControl("AmountsRecipeIngredients.json").GetJsonPathFile(), JsonConvert.SerializeObject(AmountRecipeIngredients));
                            File.WriteAllText(new JsonControl("StepsCooking.json").GetJsonPathFile(), JsonConvert.SerializeObject(StepCookings));

                            NavigateRecipeCategories navigateRecipeCategories = new NavigateRecipeCategories();
                            navigateRecipeCategories.GetRecipesCategory(CategoryRecipe, CategoryRecipe.ParentId);
                        }
                        else GetMenuItems(CategoryRecipe, RecipeViewSelected);

                    }
                    break;
            }
        }
    }
}
