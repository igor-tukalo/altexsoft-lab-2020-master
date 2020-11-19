using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using HomeTask6.Web.TagHelpers;
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
        public List<CategoryMenu> DisplayedCategories { get; set; }
        public CategoriesIndexModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;

        }

        public async Task OnGetAsync()
        {
            var allCategories = (await _categoriesController.GetAllGategoriesAsync()).OrderBy(x => x.Name);
            var items = allCategories.Select(x => new CategoryMenu() { Id = x.Id, Name = x.Name, ParentId = x.ParentId });
            DisplayedCategories = (List<CategoryMenu>)items.BuildTree();
        }

        public IActionResult OnPostRedirectCreateAsync(int categoryId)
        {
            string url = Url.Page("CreateCategory", new { categoryId });
            return Redirect(url);
        }
        public IActionResult OnPostRedirectEdit(int categoryId)
        {
            string url = Url.Page("EditCategory", new { categoryId });
            return Redirect(url);
        }
        public async Task<IActionResult> OnPostDeleteCategoryAsync(int categoryId)
        {
            await _categoriesController.DeleteCategoryAsync(categoryId);
            string url = Url.Page("CategoriesIndex");
            return Redirect(url);
        }
    }
}
