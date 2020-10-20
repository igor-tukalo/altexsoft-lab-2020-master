using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HomeTask6.Web.Pages.Ingredients
{
    public class IngredientsIndexModel : PageModel
    {
        private readonly IIngredientsController _ingredientsController;
        public List<Ingredient> _ingredients;
        public List<Ingredient> DisplayedIngredients;
        [BindProperty]
        public int TotalRecords { get; set; }

        [BindProperty]
        public int PageNo { get; set; }

        [BindProperty]
        public int PageSize { get; set; }

        public IngredientsIndexModel(IIngredientsController ingredientsController)
        {
            _ingredientsController = ingredientsController;
        }

        public async Task OnGet(int pageNo = 1, int pageSize = 10)
        {
            DisplayedIngredients = await _ingredientsController.GetAllIngredients().OrderBy(x => x.Name).Skip((pageNo - 1) * pageNo).Take(pageSize).ToListAsync();
            TotalRecords = _ingredientsController.GetAllIngredients().Count();
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
