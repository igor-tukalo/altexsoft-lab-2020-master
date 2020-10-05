using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface ICookingStepsController
    {
        Task AddAsync(int recipeId, int stepNum, string stepName);
        Task EditAsync(CookingStep cookingStep);
        Task DeleteAsync(int id);
        Task<CookingStep> GetCookingStepByIdAsync(int id);
        Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id);
    }
}
