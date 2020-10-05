using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class CookingStepsController : BaseController, ICookingStepsController
    {
        public CookingStepsController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }

        public async Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.RecipeId == id);
        }

        public async Task<List<CookingStep>> GetCookingStepByIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.Id == id);
        }

        public async Task Add(int recipeId, int stepNum, string stepName)
        {
            await UnitOfWork.Repository.AddAsync(new CookingStep() { Step = stepNum, Name = stepName, RecipeId = recipeId });
        }

        public void Edit(int id)
        {
            CookingStep cookingStep = UnitOfWork.Repository.GetById<CookingStep>(id);
            Console.Write($"    Describe the cooking step {cookingStep.Step}: ");
            string stepName = ValidManager.NullOrEmptyText(Console.ReadLine());
            cookingStep.Name = stepName;
            UnitOfWork.Repository.Update(cookingStep);
        }

        public void Delete(int id, int idRecipe)
        {
            Console.Write("    Do you really want to remove the cooking step? ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            foreach (CookingStep s in CookingSteps.Where(x => x.RecipeId == idRecipe && x.Step > UnitOfWork.Repository.GetById<CookingStep>(id).Step))
            {
                s.Step--;
            }
            UnitOfWork.Repository.Delete(UnitOfWork.Repository.GetById<CookingStep>(id));
        }
    }
}
