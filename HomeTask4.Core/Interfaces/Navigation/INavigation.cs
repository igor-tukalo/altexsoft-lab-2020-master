using HomeTask4.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeTask4.Core.Interfaces.Navigation
{
    public interface INavigation
    {
        Task ShowMenu();
        Task SelectMethodMenu(int id);
    }
}
