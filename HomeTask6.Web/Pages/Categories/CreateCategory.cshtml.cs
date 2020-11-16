using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public string Message { get; set; }
        public string Parent�ategoryName { get; set; }

        public CreateCategoryModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGet(int categoryId)
        {
            Parent�ategoryName = (await _categoriesController.GetCategoryByIdAsync(categoryId)).Name;
            Message = $"Create a category in the parent category �{Parent�ategoryName}�";
        }

        public async Task<IActionResult> OnPostCreateCategoryAsync(string nameCategory, string parent�ategoryName)
        {
            await _categoriesController.AddAsync(nameCategory, parent�ategoryName);
            string url = Url.Page("CategoriesIndex");
            return Redirect(url);
        }
    }
}
