using System;
using System.Collections.Generic;
using System.Linq;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class RecipeViewControl : RecipesCategoryControl
    {
        Recipe RecipeViewSelected;
        public RecipeViewControl(int recipeId, int categoryId, UnitOfWork _unitOfWork)
        {
            IdPrevCategory = categoryId;
            RecipeId = recipeId;
            unitOfWork = _unitOfWork;
            RecipeViewSelected = unitOfWork.Recipes.Get(RecipeId);
        }

        virtual public void GetMenuItems()
        {
            Console.Clear();
            ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Back to recipe category"),
                    new Category(name: "    Edit recipe"),
                    new Category(name: "    Delete recipe")
                };

            Console.WriteLine("\n    View recipe\n");
            RecipeView();
            CallMenuNavigation();
        }

        public void RecipeView()
        {
            //Console.Clear();
            Console.WriteLine($"{new string('\n', ItemsMenu.Count)}");
            Console.WriteLine($"{new string('\n', 5)}    ________{RecipeViewSelected.Name}________\n\n");
            Console.WriteLine($"    {Validation.WrapText(10, RecipeViewSelected.Description, "\n    ")}");
            Console.WriteLine("\n    Required ingredients:\n");

            //ingredients recipe
            if (unitOfWork.AmountIngredients.GetAll() != null)
                foreach (var a in unitOfWork.AmountIngredients.GetAll().Where(x => x.IdRecipe == RecipeViewSelected.Id))
                {
                    foreach (var i in unitOfWork.Ingredients.GetAll().Where(x => x.Id == a.IdIngredient))
                    {
                        Console.WriteLine($"    {i.Name} - {a.Amount} {a.Unit}");
                    }
                }

            //steps recipe
            Console.WriteLine("\n    Сooking steps:\n");
            foreach (var s in unitOfWork.StepsCooking.GetAll().Where(x => x.IdRecipe == RecipeViewSelected.Id).OrderBy(x => x.Step))
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
                        new NavigateRecipeCategories().GetMenuItems(unitOfWork.Recipes.Get(RecipeId).IdCategory);
                    }
                    break;
                case 1:
                    {
                        // Edit recipe
                        new RecipeСreateControl(unitOfWork.Recipes.Get(RecipeId).IdCategory, IdPrevCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        // Delete recipe
                        Console.Clear();
                        Console.Write("Are you sure you want to delete the recipe? ");
                        if (Validation.YesNo() == ConsoleKey.Y)
                        {
                            int idCategory = unitOfWork.Recipes.Get(RecipeId).IdCategory;
                            foreach (var a in unitOfWork.AmountIngredients.GetAll().ToList().Where(x => x.IdRecipe == RecipeId))
                                unitOfWork.AmountIngredients.Delete(a.Id);

                            foreach (var a in unitOfWork.StepsCooking.GetAll().ToList().Where(x => x.IdRecipe == RecipeId))
                                unitOfWork.StepsCooking.Delete(a.Id);

                            unitOfWork.Recipes.Delete(RecipeId);

                            unitOfWork.SaveAllData();
                            new NavigateRecipeCategories().GetMenuItems(idCategory);
                        }
                        else new NavigateRecipeCategories().GetMenuItems(unitOfWork.Recipes.Get(RecipeId).IdCategory);

                    }
                    break;
            }
        }
    }
}
