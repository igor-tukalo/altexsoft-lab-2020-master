using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public class CategoriesController : BaseController, ICategoriesController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings) : base(unitOfWork, settings)
        {
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            Task<Category> taskCategory = null;
            try
            {
                taskCategory = UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            return await taskCategory;
        }

        public async Task<Category> GetCategoryByParentIdAsync(int parentId)
        {
            return await UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Id == parentId);
        }

        public async Task<List<Category>> GetItemsAsync()
        {
            return await UnitOfWork.Repository.GetListAsync<Category>();
        }

        public async Task<List<Category>> GetItemsWhereParentIdAsync(int categoryId)
        {
            return await UnitOfWork.Repository.GetListWhereAsync<Category>(x => x.ParentId == categoryId);
        }

        public async Task AddAsync(string nameCategory, string parentСategoryName)
        {
            try
            {
                var category = await UnitOfWork.Repository.GetByPredicateAsync<Category>(x => x.Name == parentСategoryName);
                int idMainCategory = category.Id;
                await UnitOfWork.Repository.AddAsync(new Category() { Name = nameCategory, ParentId = idMainCategory });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        private async Task RemoveHierarchicalCategoryAsync(Category category, int level)
        {
            if (category != null)
            {
                await UnitOfWork.Repository.DeleteAsync(await GetByIdAsync(category.Id));
            }
            foreach (Category child in (await GetItemsWhereParentIdAsync(category.Id)).OrderBy(x => x.Name))
            {
                await RemoveHierarchicalCategoryAsync(child, level + 1);
            }
        }

        public async Task RenameAsync(int id, string newName)
        {
            Category category = (await GetByIdAsync(id));
            category.Name = newName;
            await UnitOfWork.Repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            Category parent = (await UnitOfWork.Repository.GetByIdAsync<Category>(id));
            if (parent.ParentId == 0)
            {
                return;
            }
            Console.Write("    Attention! Are you sure you want to delete the category? You will also delete all the recipes that are in them! ");
            if (ValidManager.YesNo() == ConsoleKey.N)
            {
                return;
            }
            await RemoveHierarchicalCategoryAsync(parent, 1);
        }
    }
}
