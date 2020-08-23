using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class RecipeIngredientsControl : IngredientsControl
    {
        public RecipeIngredientsControl(UnitOfWork _unitOfWork, int recipeId)
        {
            unitOfWork = _unitOfWork;
            RecipeId = recipeId;

            ItemsMenuMain = new List<EntityMenu>
                {
                    new Ingredient(name: "  Сreate a new ingredient"),
                    new Ingredient(name: "  Save the ingredients list and return to create the recipe"),
                    new Ingredient(name: "  Clear the ingredients list and return to create the recipe"),
                };
        }

        public override void GetMenuItems(int IdMenu = 1)
        {
            Console.Clear();

            ItemsMenu = new List<EntityMenu>(ItemsMenuMain);

            foreach (var ingredient in unitOfWork.Ingredients.GetAll().OrderBy(x => x.Name))
            {
                ItemsMenu.Add(new Ingredient() { Id = ingredient.Id, Name = $"    {ingredient.Name}" });
            }

            Console.WriteLine("\n  Edit  recipe ingredients list ");
            Console.WriteLine("\n  To add an ingredient to a recipe, select the ingredient and press ENTER");

            ViewAddedIngredients(RecipeId);

            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Сreate a new ingredient
                        new СreateIngredientWhenChangeRecipeControl(unitOfWork, RecipeId).GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        // Save the ingredients list and return to create the recipe
                        new RecipeСreateControl(RecipeId, unitOfWork.Recipes.Get(RecipeId).IdCategory, unitOfWork).GetMenuItems();
                    } 
                    break;
                case 2:
                    {
                        // Cancel and return to create the recipe
                        foreach (var a in unitOfWork.AmountIngredients.GetAll().ToList().Where(x => x.IdRecipe == RecipeId))
                            unitOfWork.AmountIngredients.Delete(a.Id);

                        new RecipeСreateControl(RecipeId, unitOfWork.Recipes.Get(RecipeId).IdCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                default:
                    {
                        if (ItemsMenu[id].Id != 0)
                        {
                            if (ItemsMenu[id].TypeEntity == "amountIngr")
                            {
                                unitOfWork.AmountIngredients.Delete(ItemsMenu[id].Id);
                            }
                            else
                            {
                                AddAmountIngredient(ItemsMenu[id].Id, RecipeId);
                            }
                            GetMenuItems();
                        }    
                    }
                    break;
            }
        }

        void ViewAddedIngredients(int idRecipe)
        {
            ItemsMenu = new List<EntityMenu>(ItemsMenuMain)
            {
                new Ingredient(name: "\n    Added ingredients:\n")
            };

            foreach (var amount in unitOfWork.AmountIngredients.GetAll().Where(x => x.IdRecipe == idRecipe))
            {
                foreach (var ingr in unitOfWork.Ingredients.GetAll().Where(x => x.Id == amount.IdIngredient))
                {
                    ItemsMenu.Add(new Category(id: amount.Id, name: $"    {ingr.Name} {amount.Amount} {amount.Unit}", typeEntity: "amountIngr"));
                }
            }

            ItemsMenu.Add(new Ingredient(name: "\n    Ingredients to add:\n"));

            foreach (var ingr in unitOfWork.Ingredients.GetAll().OrderBy(x => x.Name))
            {
                if (!unitOfWork.AmountIngredients.GetAll().Where(x => x.IdRecipe == idRecipe).ToList().Exists(x => x.IdIngredient == ingr.Id))
                    ItemsMenu.Add(new Category(id: ingr.Id, name: $"    {ingr.Name}", typeEntity: "ingr"));
            }
        }

        void AddAmountIngredient(int idIngredient, int idRecipe)
        {
            int idAmountIngredients = unitOfWork.AmountIngredients.GetAll().Count() >0? unitOfWork.AmountIngredients.GetAll().Max(x => x.Id) + 1 :1;

            Console.Write("\n Enter the amount of ingredient: ");
            double amount = Validation.ValidDouble(Console.ReadLine().Replace(".", ","));

            Console.Write(" Enter the unit of ingredient: ");
            string unit = Validation.NullOrEmptyText(Console.ReadLine());

            unitOfWork.AmountIngredients.Create(new AmountIngredient { Id = idAmountIngredients, Amount = amount, Unit = unit, IdIngredient = idIngredient, IdRecipe = idRecipe });
        }
    }
}
