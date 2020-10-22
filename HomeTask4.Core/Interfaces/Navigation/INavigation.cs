using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation
{
    public interface INavigation
    {
        Task ShowMenuAsync();
        Task SelectMethodMenuAsync(int id);
    }
}
