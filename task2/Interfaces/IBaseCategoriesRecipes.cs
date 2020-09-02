using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    public interface IBaseCategoriesRecipes
    {
        List<AmountIngredient> AmountIngredients { get; set; }
        List<Category> Categories { get; set; }
        List<CookingStep> CookingSteps { get; set; }
        List<Recipe> Recipes { get; set; }
    }
}