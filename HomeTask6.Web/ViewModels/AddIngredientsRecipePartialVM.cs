using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask6.Web.ViewModels
{
    public class AddIngredientsRecipePartialVM
    {
        public int RecipeId { get; set; }
        public List<AmountIngredient> RecipeIngredients { get; set; }
    }
}
