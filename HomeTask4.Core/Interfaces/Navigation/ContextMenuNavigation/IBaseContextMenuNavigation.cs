using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation.ContextMenuNavigation
{
    public interface IBaseContextMenuNavigation
    {
        Task ShowMenuAsync(int id);
        Task SelectMethodMenuAsync(int id);
    }
}
