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
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        private async Task RemoveHierarchicalCategoryAsync(Category category, int level)
        {
            if (category != null)
            {
                await UnitOfWork.Repository.DeleteAsync(category);
            }
            var childCategories = await GetCategoriesWhereParentIdAsync(category.Id);
            if (childCategories!=null)
            foreach (Category child in (childCategories).OrderBy(x => x.Name))
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

        public async Task EditCategoryAsync(int categoryId, string newName, int newParentId)
        {
            Category category = await GetCategoryByIdAsync(categoryId);
            category.Name = newName;
            category.ParentId = newParentId;
            await UnitOfWork.Repository.UpdateAsync(category);
        }

        public async Task<List<Category>> FindCategoriesAsync(string name)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Category>(x => x.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task DeleteCategoryAsync(int categoryId)
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
