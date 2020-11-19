using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Ingredients
{
    public class IndexIngredientsModel : PageModel
    {
        private readonly IIngredientsController _ingredientsController;
        public List<Ingredient> DisplayedIngredients { get; set; }
        [BindProperty(SupportsGet = true)]
        public int TotalRecords { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public int PageNo { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public IndexIngredientsModel(IIngredientsController ingredientsController)
        {
            _ingredientsController = ingredientsController;
        }

        public async Task OnGetAsync()
        {
            (TotalRecords, DisplayedIngredients) = await GetDataAsync();
        }

        public async Task<IActionResult> OnGetCreateIngredientPartialAsync(string ingredientName)
        {
            await _ingredientsController.AddAsync(ingredientName);
            return await OnGetViewIngredientsPartialAsync();
        }

        public async Task<IActionResult> OnGetEditIngredientPartialAsync(int ingredientId, string ingredientName)
        {
            await _ingredientsController.RenameAsync(ingredientId, ingredientName);
            return await OnGetViewIngredientsPartialAsync();
        }

        public async Task<IActionResult> OnGetDeleteIngredientPartialAsync(int ingredientId)
        {
            await _ingredientsController.DeleteAsync(ingredientId);
            return await OnGetViewIngredientsPartialAsync();
        }

        public async Task<IActionResult> OnGetViewIngredientsPartialAsync()
        {
            (TotalRecords, DisplayedIngredients) = await GetDataAsync();

            return new PartialViewResult()
            {
                ViewName = "_ViewIngredientsPartial",
                ViewData = ViewData
            };
        }

        public async Task<(int total, List<Ingredient> items)> GetDataAsync()
        {
            List<Ingredient> allingredients = await _ingredientsController.GetAllIngredients();
            int total = allingredients.Count();

            List<Ingredient> items = allingredients.OrderBy(x => x.Name).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();

            return (total, items);
        }
    }
}
