using System.Collections.Generic;
using task2.Models;

namespace task2.Interfaces
{
    interface INavigation
    {
        List<EntityMenu> ItemsMenu { get; set; }
        void CallNavigation();
        void SelectMethodMenu(int id);
    }
}