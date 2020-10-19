using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesAddModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public string Message { get; set; }
        public string Parent—ategoryName { get; set; }

        public CategoriesAddModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGet(int categoryId)
        {
            Parent—ategoryName = (await _categoriesController.GetCategoryByIdAsync(categoryId)).Name;
            Message = $"Create a category in the parent category ´{Parent—ategoryName}ª";
        }

        public async Task<IActionResult> OnPostCreateCategoryAsync(string nameCategory, string parent—ategoryName)
        {
            await _categoriesController.AddAsync(nameCategory, parent—ategoryName);
            string url = Url.Page("Index");
            return Redirect(url);
        }
    }
}
