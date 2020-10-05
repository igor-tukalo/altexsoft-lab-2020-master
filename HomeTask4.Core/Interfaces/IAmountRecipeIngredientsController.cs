using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IAmountRecipeIngredientsController
    {
        Task AddAsync(double amount, string unit, int recipeId, int ingredientId);
        Task DeleteAsync(int amountIngredientId);
        Task<List<AmountIngredient>> GetAmountIngredietsAsync(int recipeId);
        Task<string> GetAmountIngredientNameAsync(int ingredientId);
    }
}
