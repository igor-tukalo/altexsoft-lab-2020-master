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
    public class CategoriesController : BaseController, ICategoriesController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }
        private async Task RemoveHierarchicalCategoryAsync(Category category, int level)
        {
            if (category != null)
            {
                await UnitOfWork.Repository.DeleteAsync(await GetCategoryByIdAsync(category.Id));
            }
            foreach (Category child in (await GetCategoriesWhereParentIdAsync(category.Id)).OrderBy(x => x.Name))
            {
                await RemoveHierarchicalCategoryAsync(child, level + 1);
            }
        }

        #region public methods
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            Task<Category> taskCategory;
            try
            {
                taskCategory = UnitOfWork.Repository.GetByIdAsync<Category>(categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return await taskCategory;
        }

        public async Task<List<Category>> GetCategoriesWhereParentIdAsync(int categoryId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Category>(x => x.ParentId == categoryId);
        }

        public async Task AddAsync(string nameCategory, string parentСategoryName)
        {
            Category category = (await UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Name == parentСategoryName)) ?? throw new ArgumentNullException(nameof(parentСategoryName));
            int idMainCategory = category.Id;
            await UnitOfWork.Repository.AddAsync(new Category() { Name = nameCategory, ParentId = idMainCategory });
        }

        public async Task RenameAsync(int categoryId, string newName)
        {
            Category category = await GetCategoryByIdAsync(categoryId);
            category.Name = newName;
            await UnitOfWork.Repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            Category parent = await GetCategoryByIdAsync(categoryId);
            if (parent.ParentId == 0)
            {
                return;
            }
            await RemoveHierarchicalCategoryAsync(parent, 1);
        }
        #endregion
    }
}
