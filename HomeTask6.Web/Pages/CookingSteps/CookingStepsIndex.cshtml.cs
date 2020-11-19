using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask6.Web.Pages.CookingSteps
{
    public class CookingStepsIndexModel : PageModel
    {
        private readonly ICookingStepsController _cookingStepsController;
        public List<CookingStep> CookingSteps { set; get; }
        public int RecipeId { get; set; }

        public CookingStepsIndexModel(ICookingStepsController cookingStepsController)
        {
            _cookingStepsController = cookingStepsController;
        }

        public async Task OnGetAsync(int recipeId)
        {
            RecipeId = recipeId;
            CookingSteps = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId);
        }

        public async Task OnPostAddCookingStepAsync(int recipeId, string cookingStepNameAdd)
        {
            int stepNum = (await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId)).Max(x => x.Step) + 1;
            await _cookingStepsController.AddAsync(recipeId, stepNum, cookingStepNameAdd);
            await OnGetAsync(recipeId);
        }

        public async Task OnPostEditCookingStep(int recipeId, int cookingStepId, string cookingStepName)
        {
            CookingStep cookingStep = await _cookingStepsController.GetCookingStepByIdAsync(cookingStepId);
            cookingStep.Name = cookingStepName;
            await _cookingStepsController.EditAsync(cookingStep);
            await OnGetAsync(recipeId);
        }

        public async Task OnPostDeleteCookingStepAsync(int recipeId, int cookingStepId)
        {
            await _cookingStepsController.DeleteAsync(cookingStepId);
            await OnGetAsync(recipeId);
        }
    }
}
