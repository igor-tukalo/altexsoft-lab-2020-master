using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesEditModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public int CategoryId { get; set; }
        public string CurrentCategoryName { get; set; }

        public CategoriesEditModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGetAsync(int categoryId)
        {
            CategoryId = categoryId;
            CurrentCategoryName = (await _categoriesController.GetCategoryByIdAsync(CategoryId)).Name;
        }
    }
}
