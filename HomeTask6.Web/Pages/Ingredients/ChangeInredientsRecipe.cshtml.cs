using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HomeTask6.Web.Pages.Ingredients
{
    public class ChangeInredientsRecipeModel : PageModel
    {
        private readonly IIngredientsController _ingredientsController;
        private readonly IAmountRecipeIngredientsController _amountRecipeIngredientsController;

        public List<Ingredient> FoundIngredients { set; get; }
        public List<AmountIngredient> RecipeIngredients { set; get; }
        public int SelectedAmountIngredient { get; set; }
        public int RecipeId { get; set; }

        public ChangeInredientsRecipeModel(IIngredientsController ingredientsController,
            IAmountRecipeIngredientsController amountRecipeIngredientsController)
        {
            _ingredientsController = ingredientsController;
            _amountRecipeIngredientsController = amountRecipeIngredientsController;
        }

        public async Task OnGet(int recipeId)
        {
            RecipeId = recipeId;
            RecipeIngredients = (await _amountRecipeIngredientsController.GetAmountIngredietsRecipeAsync(recipeId)).OrderBy(x=>x.Ingredient.Name).ToList();
        }

        public async Task OnPostFindIngredientsAsync(string ingredientName, int recipeId)
        {
            await OnGet(recipeId);
            if (!string.IsNullOrWhiteSpace(ingredientName))
            {
                FoundIngredients = await _ingredientsController.FindIngredientsAsync(ingredientName);
            }
        }

        public async Task<PartialViewResult> OnGetFindIngredientsPartial(string ingredientName)
        {
            FoundIngredients = await _ingredientsController.FindIngredientsAsync(ingredientName);
            return new PartialViewResult
            {
                ViewName = "_ChangeInredientsRecipePartial",
                ViewData = new ViewDataDictionary<List<Ingredient>>(ViewData, FoundIngredients)
            };
        }

        public async Task OnPostAddIngredientAsync(double amount, string unit, int recipeId, int selectedAmountIngredient)
        {
            if (amount > 0)
            {
                await _amountRecipeIngredientsController.AddAsync(amount, unit, recipeId, selectedAmountIngredient);
                await OnGet(recipeId);
            }
        }

        public async Task OnPostDeleteIngredientAsync(int ingredientId, int recipeId)
        {
            await _amountRecipeIngredientsController.DeleteAsync(ingredientId);
            await OnGet(recipeId);
        }
    }
}
