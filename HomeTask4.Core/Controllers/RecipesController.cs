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
        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            return await UnitOfWork.Repository.GetByIdAsync<Recipe>(recipeId);
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Id == categoryId);
        }

        public async Task<List<Category>> GetCategoriesWhereParentIdAsync(int parentId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Category>(x => x.ParentId == parentId);
        }

        public async Task<List<Recipe>> GetRecipesWhereCategoryIdAsync(int categoryId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Recipe>(x => x.CategoryId == categoryId);
        }

        public async Task<List<string>> GetIngredientsWhereRecipeIdAsync(int recipeId)
        {
            List<AmountIngredient> amountIngredients = await UnitOfWork.Repository.GetListWhereAsync<AmountIngredient>(x => x.RecipeId == recipeId);
            List<string> ingredients = new List<string>();
            foreach (AmountIngredient amountIngredient in amountIngredients)
            {
                Ingredient ingredient = await UnitOfWork.Repository.GetByIdAsync<Ingredient>(amountIngredient.IngredientId);
                ingredients.Add($"{ingredient.Name} - {amountIngredient.Amount} {amountIngredient.Unit}");
            }
            return ingredients;
        }

        public async Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int recipeId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<CookingStep>(x => x.RecipeId == recipeId);
        }

        public async Task AddAsync(string nameRecipe, string description, int categoryId)
        {
            await UnitOfWork.Repository.AddAsync(new Recipe() { Name = nameRecipe, Description = description, CategoryId = categoryId });
        }

        public async Task RenameAsync(int recipeId, string newName)
        {
            Recipe recipe = await GetRecipeByIdAsync(recipeId);
            recipe.Name = newName;
            await UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public async Task ChangeDescriptionAsync(int recipeId, string newDesc)
        {
            Recipe recipe = await GetRecipeByIdAsync(recipeId);
            recipe.Description = newDesc;
            await UnitOfWork.Repository.UpdateAsync(recipe);
        }

        public async Task DeleteAsync(int recipeId)
        {
            Recipe recipe = await GetRecipeByIdAsync(recipeId);
            await UnitOfWork.Repository.DeleteAsync(recipe);
        }
        #endregion
    }
}
