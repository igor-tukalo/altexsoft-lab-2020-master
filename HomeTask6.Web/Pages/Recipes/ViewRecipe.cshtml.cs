using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Recipes
{
    public class ViewRecipeModel : PageModel
    {
        private readonly IRecipesController _recipesController;
        private readonly IAmountRecipeIngredientsController _amountRecipeIngredientsController;
        private readonly ICookingStepsController _cookingStepsController;
        public List<AmountIngredient> DisplayedIngredientsRecipe { get; set; }
        public List<CookingStep> DisplayedCookingStepsRecipe { get; set; }
        public Recipe Recipe { get; set; }

        public ViewRecipeModel(IRecipesController recipesController, IAmountRecipeIngredientsController amountRecipeIngredientsController, ICookingStepsController cookingStepsController)
        {
            _recipesController = recipesController;
            _cookingStepsController = cookingStepsController;
            _amountRecipeIngredientsController = amountRecipeIngredientsController;
        }

        public async Task OnGetAsync(int recipeId)
        {
            Recipe = await _recipesController.GetRecipeByIdAsync(recipeId);
            DisplayedIngredientsRecipe = await _amountRecipeIngredientsController.GetAmountIngredietsRecipeAsync(recipeId);
            DisplayedCookingStepsRecipe = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId);
        }

        public async Task<IActionResult> OnPostDeleteRecipeAsync(int recipeId, int categoryId)
        {
            await _recipesController.DeleteAsync(recipeId);
            string url = Url.Page("RecipesIndex", "GetRecipes", new { categoryId });
            return Redirect(url);
        }
    }
}
