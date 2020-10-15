using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask6.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages
{
    public class CategoriesModel : PageModel
    {
        List<Category> categories;
        List<EntityMenu> items;
        public List<EntityMenu> DisplayedCategories { get; set; }
        public CategoriesModel()
        {
            items = new List<EntityMenu>();
            categories = new List<Category>()
            {
                new Category{ Id=1, Name="Categories",  ParentId=0},
                new Category{ Id=2, Name="Bakery products",  ParentId=1},
                new Category{ Id=3, Name="Kefir baked goods",  ParentId=2},
                new Category{ Id=4, Name="Bread on kefir",  ParentId=2},
                new Category{ Id=5, Name="Fish, seafood",  ParentId=1},
            };

            BuildHierarchicalCategoriesAsync(items, categories.First(x=>x.Id==1), 1);
        }
        public void OnGet()
        {
            DisplayedCategories = items;
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

        private void BuildHierarchicalCategoriesAsync(List<EntityMenu> items, Category category, int level)
        {
            if (category != null)
            {
                items.Add(new EntityMenu() { Id = category.Id, Name = $"{new string('-', level)}{category.Name}", ParentId = category.ParentId });
            }
            List<Category> categoriesWhereParentId = categories.Where(x => x.ParentId == category.Id).ToList();
            foreach (Category child in categoriesWhereParentId.OrderBy(x => x.Name))
            {
                BuildHierarchicalCategoriesAsync(items, child, level + 1);
            }
        }
    }
}
