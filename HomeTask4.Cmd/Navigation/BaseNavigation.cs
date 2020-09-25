using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation
{
    public abstract class BaseNavigation : INavigation
    {
        internal IUnitOfWork UnitOfWork { get; set; }
        internal BaseNavigation(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public List<EntityMenu> ItemsMenu { get; set; }
        public virtual void CallNavigation()
        {
            new Navigation().CallNavigation(ItemsMenu, SelectMethodMenu);
        }
        public virtual void SelectMethodMenu(int id) { }
    }
}
