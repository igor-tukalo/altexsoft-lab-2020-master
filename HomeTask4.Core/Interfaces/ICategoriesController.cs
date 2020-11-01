using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface ICategoriesController
    {
        Task AddAsync(string nameCategory, string parentСategoryName);
        Task<List<Category>> GetCategoriesWhereParentIdAsync(int categoryId);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<List<Category>> FindCategoriesAsync(string name);
        Task EditCategoryAsync(int categoryId, string newName, int newParentId);
        Task DeleteCategoryAsync(int categoryId);
        Task<List<Category>> GetAllGategoriesAsync();
    }
}
