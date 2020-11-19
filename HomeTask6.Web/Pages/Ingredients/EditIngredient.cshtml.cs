using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Ingredients
{
    public class EditIngredientModel : PageModel
    {
        private readonly IIngredientsController _ingredientsController;
        public EditIngredientModel(IIngredientsController ingredientsController)
        {
            _ingredientsController = ingredientsController;
        }

        public async Task<IActionResult> OnPostSaveIngredientChangesAsync(int pageNo, int ingredientId, string ingredientName)
        {
            await _ingredientsController.RenameAsync(ingredientId, ingredientName);
            string url = Url.Page("IngredientsIndex", new { pageNo });
            return Redirect(url);
        }
    }
}
