using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface IBaseCategoriesRecipes
    {
        List<AmountIngredient> AmountIngredients { get; }
        List<Category> Categories { get; }
        List<CookingStep> CookingSteps { get; }
        List<Recipe> Recipes { get; }
    }
}
