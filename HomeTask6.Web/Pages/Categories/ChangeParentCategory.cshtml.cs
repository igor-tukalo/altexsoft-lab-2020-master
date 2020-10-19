using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Categories
{
    public class ChangeParentCategoryModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public List<Category> FoundCategories { set; get; }

        public ChangeParentCategoryModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnPostFindCategoriesAsync(int categoryId, string nameRootCategory)
        {
            if (!string.IsNullOrWhiteSpace(nameRootCategory))
            {
                FoundCategories = new List<Category>();
                FoundCategories = (await _categoriesController.FindCategoriesAsync(nameRootCategory)).Where(x => x.Id != categoryId && x.ParentId != categoryId).ToList();
            }
        }

        public async Task<IActionResult> OnPostSaveCategoryChangesAsync(int categoryId, string nameCategory, int parentCategoryId)
        {
            await _categoriesController.EditCategoryAsync(categoryId, nameCategory, parentCategoryId);
            string url = Url.Page("Index");
            return Redirect(url);
        }
    }
}
