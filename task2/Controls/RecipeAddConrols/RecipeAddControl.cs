using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task2.Instruments;
using task2.Models;

namespace task2.Controls.RecipeAddConrols
{
    public class RecipeAddControl : RecipeAddIngredientsControl
    {
        public RecipeAddControl()
        {

        }

        public void AddRecipe(EntityMenu category)
        {
            try
            {
                Console.WriteLine($"\n The recipe will be added to the category: {category.Name.Replace("-", "")}");

                int idRecipe = Recipes.Max(x => x.Id) + 1;

                Console.Write("\n Enter the name of the recipe: ");
                string nameRecipe = Validation.IsExsistsNameList(new List<EntityMenu>(Recipes), Console.ReadLine());

                Console.Write(" Enter recipe description: ");
                string description = Validation.NullOrEmptyText(Console.ReadLine());

                Recipe addedRecipe = new Recipe() { Id = idRecipe, Name = nameRecipe, Description = description, IdCategory = category.Id };


                RecipeAddIngredientsControl ingredientsRecipeControl = new RecipeAddIngredientsControl();
                ingredientsRecipeControl.GetMenuIngredientsChangeBeforeAdding(addedRecipe, category);
            }
            catch (Exception ex)
            { Console.WriteLine($"{ex.Message}"); }
        }
    }
}
