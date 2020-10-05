using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface ICookingStepsController
    {
        Task Add(int recipeId, int stepNum, string stepName);
        Task Edit(int id);
        Task Delete(int id, int idRecipe);
        Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id);
    }
}
