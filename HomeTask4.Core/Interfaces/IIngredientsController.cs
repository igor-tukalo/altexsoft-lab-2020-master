using HomeTask4.Core.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IIngredientsController : IBaseController<Ingredient>
    {
        Task AddAsync(string name);
        Task<List<IEnumerable<Ingredient>>> GetItemsBatchAsync();
    }
}
