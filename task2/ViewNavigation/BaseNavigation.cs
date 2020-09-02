using System.Collections.Generic;
using task2.Interfaces;
using task2.Models;

namespace task2.ViewNavigation.ContextMenuNavigation
{
    abstract class BaseNavigation : INavigation
    {
        public List<EntityMenu> ItemsMenu { get; set; }
        public virtual void CallNavigation()
        {
            new Navigation().CallNavigation(ItemsMenu, SelectMethodMenu);
        }
        public virtual void SelectMethodMenu(int id) { }
    }
}
