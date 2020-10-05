using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class RecipesController : BaseController, IRecipesController
    {
        public RecipesController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }

        #region public methods
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetByPredicateAsync<Recipe>(x => x.Id == id);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Id == id);
        }

        public async Task<List<Category>> GetCategoriesWhereParentIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Category>(x => x.ParentId == id);
        }

        public async Task<List<Recipe>> GetRecipessWhereCategoryIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Recipe>(x => x.CategoryId == id);
        }

        public async Task<List<string>> GetIngredientsWhereRecipeIdAsync(int id)
        {
            List<AmountIngredient> amountIngredients = await UnitOfWork.Repository.GetListWhereAsync<AmountIngredient>(x => x.RecipeId == id);
            List<string> ingredients = new List<string>();
            foreach (AmountIngredient amountIngredient in amountIngredients)
            {
                Ingredient ingredient = await UnitOfWork.Repository.GetByIdAsync<Ingredient>(amountIngredient.IngredientId);
                ingredients.Add($"{ingredient.Name} - {amountIngredient.Amount} {amountIngredient.Unit}");
            }
            return ingredients;
        }

        public async Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.RecipeId == id);
        }

        public async Task AddAsync(string nameRecipe, string description, int categoryId)
        {
            await UnitOfWork.Repository.AddAsync(new Recipe() { Name = nameRecipe, Description = description, CategoryId = categoryId });
        }

        public async Task RenameAsync(int id, string newName)
        {
            Recipe recipe = await GetRecipeByIdAsync(id);
            recipe.Name = newName;
            await UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public async Task ChangeDescription(int id, string newDesc)
        {
            Recipe recipe = await GetRecipeByIdAsync(id);
            recipe.Description = newDesc;
            await UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public async Task DeleteAsync(int id)
        {
            Recipe recipe = await GetRecipeByIdAsync(id);
            await UnitOfWork.Repository.DeleteAsync(recipe);
        }
        #endregion
    }
}
