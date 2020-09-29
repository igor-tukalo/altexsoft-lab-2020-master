using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces.Navigation
{
    public interface IBaseNavigation
    {
        List<EntityMenu> ItemsMenu { get; }
        void CallNavigation();
        void SelectMethodMenu(int id);
    }
}
