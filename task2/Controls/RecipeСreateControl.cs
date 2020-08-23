using System;
using System.Collections.Generic;
using task2.Instruments;
using task2.Models;
using task2.Repositories;

namespace task2.Controls
{
    public class RecipeСreateControl : RecipeViewControl
    {
        public RecipeСreateControl(int recipeId, int categoryId, UnitOfWork _unitOfWork) : base(recipeId, categoryId, _unitOfWork)
        {
        }

        public override void GetMenuItems()
        {
            Console.Clear();

            ItemsMenu = new List<EntityMenu>
                {
                    new Category(name: "    Edit recipe name"),
                    new Category(name: "    Edit recipe description"),
                    new Category(name: "    Edit ingredients list"),
                    new Category(name: "    Edit cooking steps"),
                    new Category(name: "    Save changes and create recipe"),
                    new Category(name: "    Cancel and return to recipe category")
                };

            Console.WriteLine($"\n The recipe is in the category: {unitOfWork.Categories.Get(IdPrevCategory).Name}");
            RecipeView();
            CallMenuNavigation();
        }

        protected override void SelectMethodMenu(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        // Add / Сhange recipe name
                        EditRecipeName(RecipeId);
                        new RecipeСreateControl(RecipeId, IdPrevCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                case 1:
                    {
                        // Add / Сhange recipe description
                        EditRecipeDescription(RecipeId);
                        new RecipeСreateControl(RecipeId, IdPrevCategory, unitOfWork).GetMenuItems();
                    }
                    break;
                case 2:
                    {
                        // Add / Сhange ingredients list
                        new RecipeIngredientsControl(unitOfWork, RecipeId).GetMenuItems();
                    }
                    break;
                case 3:
                    {
                        // Add / Сhange cooking steps
                        new RecipeStepsCookingControl(unitOfWork, RecipeId).GetMenuItems();
                    }
                    break;
                case 4:
                    {
                        //  Save changes and create recipe
                        if (!string.IsNullOrWhiteSpace(unitOfWork.Recipes.Get(RecipeId).Name))
                        {
                            unitOfWork.SaveAllData();
                            new NavigateRecipeCategories().GetMenuItems(IdPrevCategory);
                        }
                        else Console.WriteLine(" To create a recipe, enter the recipe name!");
                    }
                    break;
                case 5:
                    {
                        // Cancel and return to main menu
                        new NavigateRecipeCategories().GetMenuItems(IdPrevCategory);
                    }
                    break;
            }
        }

        private void EditRecipeName(int idRecipe)
        {
            Console.Write("\n    Enter the name of the recipe: ");
            string nameRecipe = Validation.IsNameMustNotExist(new List<EntityMenu>(unitOfWork.Recipes.GetAll()), Console.ReadLine());
            Recipe recipe = unitOfWork.Recipes.Get(idRecipe);
            unitOfWork.Recipes.Update(new Recipe { Id = recipe.Id, Name = nameRecipe, Description = recipe.Description, IdCategory = recipe.IdCategory });
        }

        private void EditRecipeDescription(int idRecipe)
        {
            Console.Write("\n    Enter recipe description: ");
            string description = Validation.NullOrEmptyText(Console.ReadLine());
            Recipe recipe = unitOfWork.Recipes.Get(idRecipe);
            unitOfWork.Recipes.Update(new Recipe { Id = recipe.Id, Name = recipe.Name, Description = description, IdCategory = recipe.IdCategory });
        }
    }
}
