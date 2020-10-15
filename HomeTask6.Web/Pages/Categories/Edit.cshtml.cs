using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeTask6.Web.Pages.Categories
{
    public class CategoriesEditModel : PageModel
    {
        public string Message { get; set; }
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            Message = "Edit category";
        }
        public IActionResult OnPost(string nameCategory)
        {
            return Content($"Category name changed to {nameCategory}!");
        }
    }
}
