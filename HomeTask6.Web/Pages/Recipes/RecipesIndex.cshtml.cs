using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using HomeTask6.Web.TagHelpers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.Recipes
{
    public class RecipesIndexModel : PageModel
    {
        private readonly IRecipesController _recipesController;
        private readonly ICategoriesController _categoriesController;
        public List<CategoryMenu> DisplayedCategories { get; set; }
        public List<Recipe> DisplayedRecipes { get; set; }
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }
        public string NameRecipe { get; set; }
        public string Description { get; set; }

        public RecipesIndexModel(IRecipesController recipesController, ICategoriesController categoriesController)
        {
            DisplayedCategories = new List<CategoryMenu>();
            DisplayedRecipes = new List<Recipe>();
            _recipesController = recipesController;
            _categoriesController = categoriesController;
        }

        public async Task OnGetAsync()
        {
            IOrderedEnumerable<Category> allCategories = (await _categoriesController.GetAllGategoriesAsync()).OrderBy(x => x.Name);
            IEnumerable<CategoryMenu> items = allCategories.Select(x => new CategoryMenu() { Id = x.Id, Name = x.Name, ParentId = x.ParentId });
            DisplayedCategories = (List<CategoryMenu>)items.BuildTree();
        }

        public async Task OnGetGetRecipesAsync(int categoryId)
        {
            await GetRecipes(categoryId);
        }

        public async Task OnPostGetRecipesAsync(int categoryId)
        {
            await GetRecipes(categoryId);
        }

        private async Task GetRecipes(int categoryId)
        {
            Category category = await _categoriesController.GetCategoryByIdAsync(categoryId);
            await OnGetAsync();
            CategoryId = category.ParentId != null ? categoryId : 0;
            NameCategory = category.Name;
            DisplayedRecipes = await _recipesController.GetRecipesWhereCategoryIdAsync(categoryId);
        }
    }
}