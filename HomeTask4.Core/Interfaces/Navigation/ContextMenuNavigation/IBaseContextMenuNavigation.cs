using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation
{
    public interface IBaseContextMenuNavigation
    {
        Task ShowMenu(int id);
        Task SelectMethodMenu(int id);
    }
}
