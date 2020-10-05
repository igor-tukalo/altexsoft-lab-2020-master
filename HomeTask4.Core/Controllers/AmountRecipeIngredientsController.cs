using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class AmountRecipeIngredientsController : BaseController, IAmountRecipeIngredientsController
    {
        public AmountRecipeIngredientsController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }
        public async Task AddAsync(double amount, string unit, int recipeId, int ingredientId)
        {
            AmountIngredient amountIngredient = new AmountIngredient() { Amount = amount, Unit = unit, RecipeId = recipeId, IngredientId = ingredientId };
            await UnitOfWork.Repository.AddAsync(amountIngredient);
        }

        public async Task DeleteAsync(int amountIngredientId)
        {
            AmountIngredient amountIngredient = await UnitOfWork.Repository.GetByIdAsync<AmountIngredient>(amountIngredientId);
            await UnitOfWork.Repository.DeleteAsync(amountIngredient);
        }

        public async Task<List<AmountIngredient>> GetAmountIngredietsAsync(int recipeId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<AmountIngredient>(x => x.RecipeId == recipeId);
        }

        public async Task<string> GetAmountIngredientNameAsync(int ingredientId)
        {
            return (await UnitOfWork.Repository.GetByIdAsync<Ingredient>(ingredientId)).Name;
        }
    }
}
