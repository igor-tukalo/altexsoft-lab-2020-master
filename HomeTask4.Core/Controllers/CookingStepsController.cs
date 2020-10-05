using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class CookingStepsController : BaseController, ICookingStepsController
    {
        public CookingStepsController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }

        #region public methods
        public async Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.RecipeId == id);
        }

        public async Task<CookingStep> GetCookingStepByIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetByIdAsync<CookingStep>(id);
        }

        public async Task AddAsync(int recipeId, int stepNum, string stepName)
        {
            await UnitOfWork.Repository.AddAsync(new CookingStep() { Step = stepNum, Name = stepName, RecipeId = recipeId });
        }

        public async Task EditAsync(CookingStep cookingStep)
        {
            await UnitOfWork.Repository.UpdateAsync(cookingStep);
        }

        public async Task DeleteAsync(int id)
        {
            CookingStep stepRecipe = await GetCookingStepByIdAsync(id);
            List<CookingStep> cookingStepsRecipe = await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.RecipeId == stepRecipe.RecipeId && x.Step > stepRecipe.Step);
            foreach (CookingStep cookingStep in cookingStepsRecipe)
            {
                cookingStep.Step--;
            }
            await UnitOfWork.SaveChanges();
            await UnitOfWork.Repository.DeleteAsync(stepRecipe);
        } 
        #endregion
    }
}
