using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Recipes
{
    public class CreateRecipeModel : PageModel
    {
        private readonly IRecipesController _recipesController;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public CreateRecipeModel(IRecipesController recipesController)
        {
            _recipesController = recipesController;
        }

        public void OnGet(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        public async Task<IActionResult> OnPostCreateRecipeAsync(string nameRecipe, string descRecipe, int categoryId)
        {
            await _recipesController.AddAsync(nameRecipe, descRecipe, categoryId);
            int recipeId = (await _recipesController.GetRecipesWhereCategoryIdAsync(categoryId)).Max(x => x.Id);
            string url = Url.Page("ViewRecipe", new { recipeId });
            return Redirect(url);
        }
    }
}
