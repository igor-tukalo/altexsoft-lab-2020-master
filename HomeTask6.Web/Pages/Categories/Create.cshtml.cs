using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesAddModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        private int _categoryId;
        public string Message { get; set; }
        public string Parent—ategoryName { get; set; }

        public CategoriesAddModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGet(int id)
        {
            _categoryId = id;
            Parent—ategoryName = (await _categoriesController.GetCategoryByIdAsync(_categoryId)).Name;
            Message = $"Create category to category ´{Parent—ategoryName}ª";
        }

        public async Task<IActionResult> OnPostCreateCategory(string nameCategory, string parent—ategoryName)
        {
            await _categoriesController.AddAsync(nameCategory, parent—ategoryName);
            string url = Url.Page("Index");
            return Redirect(url);
        }
    }
}
