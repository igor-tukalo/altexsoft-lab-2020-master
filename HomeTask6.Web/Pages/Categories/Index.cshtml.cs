using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        public List<EntityMenu> DisplayedCategories { get; set; }
        public CategoriesModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;

        }
        private async Task BuildHierarchicalCategoriesAsync(List<EntityMenu> items, Category category, int level)
        {
            if (items != null && category != null)
            {
                items.Add(new EntityMenu() { Id = category.Id, Separator= $"{new string('_', level*2)}", Name = category.Name, ParentId = category.ParentId });
            }
            List<Category> categoriesWhereParentId = await _categoriesController.GetCategoriesWhereParentIdAsync(category.Id);
            foreach (Category child in categoriesWhereParentId.OrderBy(x => x.Name))
            {
                await BuildHierarchicalCategoriesAsync(items, child, level + 1);
            }
        }
        public async Task OnGet()
        {
            DisplayedCategories = new List<EntityMenu>();
            Category category = await _categoriesController.GetCategoryByIdAsync(1);
            await BuildHierarchicalCategoriesAsync(DisplayedCategories, category, 1);
        }

        public IActionResult OnPostRedirectCreate(int categoryId)
        {
            string url = Url.Page("Create", new { Id = categoryId});
            return Redirect(url);
        }
        public IActionResult OnPostRedirectEdit(int categoryId)
        {
            string url = Url.Page("Edit", new { Id = categoryId });
            return Redirect(url);
        }
        public IActionResult OnPostRedirectDelete(int categoryId)
        {
            string url = Url.Page("Index");
            return Redirect(url);
        }

    }
}