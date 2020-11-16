using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Recipes
{
    public class EditRecipeModel : PageModel
    {
        private readonly IRecipesController _recipesController;
        public Recipe Recipe { get; set; }
        public EditRecipeModel(IRecipesController recipesController)
        {
            _recipesController = recipesController;
        }

        public async Task OnGet(int recipeId)
        {
            Recipe = await _recipesController.GetRecipeByIdAsync(recipeId);
        }

        public async Task<IActionResult> OnPostEditRecipeAsync(int recipeId, string nameRecipe, string descRecipe)
        {
            await _recipesController.RenameAsync(recipeId, nameRecipe);
            await _recipesController.ChangeDescriptionAsync(recipeId, descRecipe);
            string url = Url.Page("ViewRecipe", new { recipeId });
            return Redirect(url);
        }
    }
}
