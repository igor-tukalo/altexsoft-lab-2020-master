using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IIngredientsController : IBaseController<Ingredient>
    {
        Task AddAsync(string name);
        Task<List<IEnumerable<Ingredient>>> GetIngredientsBatchAsync();
        Task<List<string>> GetIngredientsWhereRecipeIdAsync(int recipeId);
        Task<List<Ingredient>> GetAllIngredients();
    }
}
