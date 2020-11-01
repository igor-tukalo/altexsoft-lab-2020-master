using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class RecipesController : BaseController, IRecipesController
    {
        public RecipesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region public methods
        public async Task<Recipe> GetRecipeByIdAsync(int recipeId)
        {
            return await UnitOfWork.Repository.GetByIdAsync<Recipe>(recipeId);
        }

        public async Task<List<Recipe>> GetRecipesWhereCategoryIdAsync(int categoryId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Recipe>(x => x.CategoryId == categoryId);
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
