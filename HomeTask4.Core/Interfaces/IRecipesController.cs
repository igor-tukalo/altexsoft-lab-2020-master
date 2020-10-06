﻿using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IRecipesController
    {
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> GetCategoriesWhereParentIdAsync(int id);
        Task<List<Recipe>> GetRecipesWhereCategoryIdAsync(int id);
        Task<List<string>> GetIngredientsWhereRecipeIdAsync(int id);
        Task<List<CookingStep>> GetCookingStepsWhereRecipeIdAsync(int id);
        Task AddAsync(string nameRecipe, string description, int categoryId);
        Task RenameAsync(int id, string newName);
        Task ChangeDescription(int id, string newDesc);
        Task DeleteAsync(int id);
    }
}
