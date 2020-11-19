using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Ingredients
{
    public class CreateIngredientModel : PageModel
    {
        private readonly IIngredientsController _ingredientsController;

        public CreateIngredientModel(IIngredientsController ingredientsController)
        {
            _ingredientsController = ingredientsController;
        }

        public async Task<IActionResult> OnPostCreateIngredientAsync(string nameIngredient)
        {
            await _ingredientsController.AddAsync(nameIngredient);
            string url = Url.Page("IngredientsIndex");
            return Redirect(url);
        }
    }
}
