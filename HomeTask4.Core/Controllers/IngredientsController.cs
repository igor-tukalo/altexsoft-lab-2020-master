﻿using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class IngredientsController : BaseController, IIngredientsController
    {
        public IngredientsController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }

        #region public methods
        public async Task<Ingredient> GetIngredientByIdAsync(int ingredientId)
        {
            return await UnitOfWork.Repository.GetByIdAsync<Ingredient>(ingredientId);
        }

        public async Task<List<IEnumerable<Ingredient>>> GetIngredientsBatchAsync()
        {
            int batchSize = CustomSettingsApp.Value.NumberConsoleLines;
            List<IEnumerable<Ingredient>> batchList = (await UnitOfWork.Repository.GetListAsync<Ingredient>()).OrderBy(x => x.Name).Batch(batchSize).ToList();
            return batchList;
        }

        public async Task AddAsync(string name)
        {
            await UnitOfWork.Repository.AddAsync(new Ingredient() { Name = name });
        }

        public async Task RenameAsync(int ingredientId, string newName)
        {
            Ingredient ingredient = await GetIngredientByIdAsync(ingredientId);
            ingredient.Name = newName;
            await UnitOfWork.Repository.UpdateAsync(ingredient);
        }
        public async Task DeleteAsync(int ingredientId)
        {
            Ingredient ingredient = await GetIngredientByIdAsync(ingredientId);
            await UnitOfWork.Repository.DeleteAsync(ingredient);
        }
        #endregion
    }
}
