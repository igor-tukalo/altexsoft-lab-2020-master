using System.Threading.Tasks;

namespace HomeTask4.Cmd.Interfaces.ContextMenuNavigation
{
    public interface IBaseContextMenuNavigation
    {
        Task ShowMenuAsync(int id);
        Task SelectMethodMenuAsync(int id);
    }
}
