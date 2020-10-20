using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask6.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            _ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Samsung Galaxy III" },
                new Ingredient { Id = 2, Name = "Samsung Ace II" },
                new Ingredient { Id = 3, Name = "HTC Hero" },
                new Ingredient { Id = 4, Name = "HTC One S" },
                new Ingredient { Id = 5, Name = "HTC One X" },
                new Ingredient { Id = 6, Name = "LG Optimus 3D" },
                new Ingredient { Id = 7, Name = "Nokia N9" },
                new Ingredient { Id = 8, Name = "Samsung Galaxy Nexus" },
                new Ingredient { Id = 9, Name = "Sony Xperia X10" },
                new Ingredient { Id = 10, Name = "Samsung Galaxy II" },
                                new Ingredient { Id = 1, Name = "Samsung Galaxy III" },
                new Ingredient { Id = 2, Name = "Samsung Ace II" },
                new Ingredient { Id = 3, Name = "HTC Hero" },
                new Ingredient { Id = 4, Name = "HTC One S" },
                new Ingredient { Id = 5, Name = "HTC One X" },
                new Ingredient { Id = 6, Name = "LG Optimus 3D" },
                new Ingredient { Id = 7, Name = "Nokia N9" },
                new Ingredient { Id = 8, Name = "Samsung Galaxy Nexus" },
                new Ingredient { Id = 9, Name = "Sony Xperia X10" },
                new Ingredient { Id = 10, Name = "Samsung Galaxy II" },
                                new Ingredient { Id = 1, Name = "Samsung Galaxy III" },
                new Ingredient { Id = 2, Name = "Samsung Ace II" },
                new Ingredient { Id = 3, Name = "HTC Hero" },
                new Ingredient { Id = 4, Name = "HTC One S" },
                new Ingredient { Id = 5, Name = "HTC One X" },
                new Ingredient { Id = 6, Name = "LG Optimus 3D" },
                new Ingredient { Id = 7, Name = "Nokia N9" },
                new Ingredient { Id = 8, Name = "Samsung Galaxy Nexus" },
                new Ingredient { Id = 9, Name = "Sony Xperia X10" },
                new Ingredient { Id = 10, Name = "Samsung Galaxy II" },
                                new Ingredient { Id = 1, Name = "Samsung Galaxy III" },
                new Ingredient { Id = 2, Name = "Samsung Ace II" },
                new Ingredient { Id = 3, Name = "HTC Hero" },
                new Ingredient { Id = 4, Name = "HTC One S" },
                new Ingredient { Id = 5, Name = "HTC One X" },
                new Ingredient { Id = 6, Name = "LG Optimus 3D" },
                new Ingredient { Id = 7, Name = "Nokia N9" },
                new Ingredient { Id = 8, Name = "Samsung Galaxy Nexus" },
                new Ingredient { Id = 9, Name = "Sony Xperia X10" },
                new Ingredient { Id = 10, Name = "Samsung Galaxy II" }
            };
        }

        public void OnGet(int pageNo = 1, int pageSize = 10)
        {
            DisplayedIngredients = _ingredients.OrderBy(x => x.Name).Skip((pageNo - 1) * pageNo).Take(pageSize).ToList();
            TotalRecords = _ingredients.Count;
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
