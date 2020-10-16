using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesEditModel : PageModel
    {
        private readonly ICategoriesController _categoriesController;
        private int _categoryId;
        public string CurrentCategoryName { get; set; }
        public string ParentÑategoryName { get; set; }
        public List<SelectListItem> FoundCategories { set; get; }
        public int SelectedFoundCategory { set; get; }

        public CategoriesEditModel(ICategoriesController categoriesController)
        {
            _categoriesController = categoriesController;
        }

        public async Task OnGet(int id)
        {
            _categoryId = id;
            CurrentCategoryName = (await _categoriesController.GetCategoryByIdAsync(_categoryId)).Name;
            int parentCategoryId = (await _categoriesController.GetCategoryByIdAsync(_categoryId)).ParentId;
            if (parentCategoryId != 0)
                ParentÑategoryName = (await _categoriesController.GetCategoryByIdAsync(parentCategoryId)).Name;
        }

        public async Task<IActionResult> OnPost(string nameCategory, string nameRootCategory)
        {
            await _categoriesController.RenameAsync(_categoryId, nameCategory);

            string url = Url.Page("Index");
            return Redirect(url);
        }

        public async Task OnPostFindCategories(string nameRootCategory, string nameCategory)
        {
            CurrentCategoryName = nameCategory;
            ParentÑategoryName = nameRootCategory;
            var foundÑategories = await _categoriesController.FindCategoriesAsync(nameRootCategory);
            FoundCategories = new List<SelectListItem>();
            foreach (var foundCategory in foundÑategories)
                FoundCategories.Add(new SelectListItem() { Text = foundCategory.Name, Value = foundCategory.Id.ToString() });
        }
    }
}
