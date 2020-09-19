using HomeTask4.Core.Entities;
using HomeTask4.Core.Interfaces;
using HomeTask4.Core.Repositories;
using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel;
using System;
using System.Collections.Generic;

namespace HomeTask4.Cmd.Navigation
{
    internal abstract class BaseNavigation : INavigation, IDisposable
    {
        private readonly ApplicationContext db;
        internal UnitOfWork UnitOfWork { get; set; }
        protected Validation ValidManager { get; }


        internal BaseNavigation()
        {
            db = new ApplicationContext();
            UnitOfWork = new UnitOfWork(db);
            ValidManager = new Validation();

        }

        protected void RefreshData()
        {
            UnitOfWork = new UnitOfWork(db);
        }

        public List<EntityMenu> ItemsMenu { get; set; }
        public virtual void CallNavigation()
        {
            new Navigation().CallNavigation(ItemsMenu, SelectMethodMenu);
        }
        public virtual void SelectMethodMenu(int id) { }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
