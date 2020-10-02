using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface ICategoriesController : IBaseController<Category>
    {
        Task AddAsync(string nameCategory, string parentСategoryName);
        Task<List<Category>> GetItemsWhereParentIdAsync(int categoryId);
        Task<Category> GetByIdAsync(int id);
    }
}
