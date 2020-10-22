using HomeTask4.SharedKernel;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces
{
    public interface IBaseController<T> where T : BaseEntity
    {
        Task RenameAsync(int id, string newName);
        Task DeleteAsync(int id);
    }
}
