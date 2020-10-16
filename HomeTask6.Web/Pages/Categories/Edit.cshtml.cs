using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesEditModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        private int _categoryId;
        public string Message { get; set; }
        public string Parent—ategoryName { get; set; }
        public string[] Categories { get; set; }

        public CategoriesEditModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGet(int id)
        {
            _categoryId = id;
            Parent—ategoryName = (await _categoriesController.GetCategoryByIdAsync(_categoryId)).Name;
            Categories = new string[] { Parent—ategoryName };
            Message = "Edit category";
        }

        public async Task<IActionResult> OnPost(string nameCategory, string nameRootCategory)
        {
            await _categoriesController.RenameAsync(_categoryId, nameCategory);
            string url = Url.Page("Index");
            return Redirect(url);
        }

        public async Task<IActionResult> OnPostFindCategories(string nameCategory)
        {
            await _categoriesController.RenameAsync(_categoryId, nameCategory);
            string url = Url.Page("Index");
            return Redirect(url);
        }
    }
}
