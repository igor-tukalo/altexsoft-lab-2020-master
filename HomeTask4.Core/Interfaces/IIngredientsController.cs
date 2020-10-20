using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IIngredientsController : IBaseController<Ingredient>
    {
        Task AddAsync(string name);
        Task<List<IEnumerable<Ingredient>>> GetIngredientsBatchAsync();
        Task<List<string>> GetIngredientsWhereRecipeIdAsync(int recipeId);
        IQueryable<Ingredient> GetAllIngredients();
    }
}
