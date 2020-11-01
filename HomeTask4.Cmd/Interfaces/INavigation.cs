using System.Threading.Tasks;

namespace HomeTask4.Cmd.Interfaces
{
    public interface INavigation
    {
        Task ShowMenuAsync();
        Task SelectMethodMenuAsync(int id);
    }
}
