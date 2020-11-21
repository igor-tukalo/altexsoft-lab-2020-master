using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        public void OnGetAsync(int recipeId)
        {
            RecipeId = recipeId;
        }

        public async Task<IActionResult> OnGetViewCookingStepsPartialAsync(int recipeId)
        {
            CookingSteps = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId);

            return new PartialViewResult
            {
                ViewName = "_ViewCookingStepsPartial",
                ViewData = new ViewDataDictionary<List<CookingStep>>(ViewData, CookingSteps)
            };
        }

        public async Task<IActionResult> OnGetAddCookingStepPartialAsync(string cookingStepName, int recipeId)
        {
            List<CookingStep> cookingStepsRecipe = await _cookingStepsController.GetCookingStepsWhereRecipeIdAsync(recipeId);
            int stepNum = cookingStepsRecipe.Count > 0 ? cookingStepsRecipe.Max(x => x.Step) + 1 : 1;
            await _cookingStepsController.AddAsync(recipeId, stepNum, cookingStepName);
            return await OnGetViewCookingStepsPartialAsync(recipeId);
        }

        public async Task<IActionResult> OnGetEditCookingStepPartialAsync(int cookingStepId, string cookingStepName, int recipeId)
        {
            CookingStep cookingStep = await _cookingStepsController.GetCookingStepByIdAsync(cookingStepId);
            cookingStep.Name = cookingStepName;
            await _cookingStepsController.EditAsync(cookingStep);
            return await OnGetViewCookingStepsPartialAsync(recipeId);
        }

        public async Task<IActionResult> OnGetDeleteCookingStepPartialAsync(int cookingStepId, int recipeId)
        {
            await _cookingStepsController.DeleteAsync(cookingStepId);
            return await OnGetViewCookingStepsPartialAsync(recipeId);
        }
    }
}
