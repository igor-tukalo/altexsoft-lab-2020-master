using HomeTask4.Core.Entities;
using System.Collections.Generic;

namespace HomeTask4.Core.Interfaces
{
    public interface INavigation
    {
        List<EntityMenu> ItemsMenu { get; }
        void CallNavigation();
        void SelectMethodMenu(int id);
    }
}
