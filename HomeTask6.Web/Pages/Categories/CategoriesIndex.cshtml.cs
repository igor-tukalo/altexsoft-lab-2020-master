using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages
{
    public class CategoriesIndexModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public List<EntityMenu> DisplayedCategories { get; set; }
        public CategoriesIndexModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;

        }
        private async Task BuildHierarchicalCategoriesAsync(List<EntityMenu> items, Category category, int level)
        {
            if (items != null && category != null)
            {
                string separator = category.Id != 1 ? $"{new string('_', level * 2)}" : "";
                items.Add(new EntityMenu() { Id = category.Id, Separator = separator, Name = category.Name, ParentId = category.ParentId });
            }
            List<Category> categoriesWhereParentId = await _categoriesController.GetCategoriesWhereParentIdAsync(category.Id);
            foreach (Category child in categoriesWhereParentId.OrderBy(x => x.Name))
            {
                await BuildHierarchicalCategoriesAsync(items, child, level + 1);
            }
        }

        public async Task OnGetAsync()
        {
            DisplayedCategories = new List<EntityMenu>();
            Category category = await _categoriesController.GetCategoryByIdAsync(1);
            await BuildHierarchicalCategoriesAsync(DisplayedCategories, category, 1);
        }

        public IActionResult OnPostRedirectCreateAsync(int categoryId)
        {
            string url = Url.Page("Create", new { categoryId });
            return Redirect(url);
        }
        public IActionResult OnPostRedirectEdit(int categoryId)
        {
            string url = Url.Page("Edit", new { categoryId });
            return Redirect(url);
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync(int categoryId)
        {
            await _categoriesController.DeleteCategoryAsync(categoryId);
            string url = Url.Page("Index");
            return Redirect(url);
        }

    }
}
