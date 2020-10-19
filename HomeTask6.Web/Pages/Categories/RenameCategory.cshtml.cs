using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Categories
{
    public class RenameCategoryModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public Category SelectedCategory { get; set; }
        public string NameCategory { get; set; }
        public RenameCategoryModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGetAsync(int categoryId)
        {
            SelectedCategory = await _categoriesController.GetCategoryByIdAsync(categoryId);
            NameCategory = SelectedCategory.Name;
        }

        public async Task<IActionResult> OnPostSaveCategoryChangesAsync(int categoryId, string nameCategory, int parentCategoryId)
        {
            await _categoriesController.EditCategoryAsync(categoryId, nameCategory, parentCategoryId);
            string url = Url.Page("Index");
            return Redirect(url);
        }
    }
}
