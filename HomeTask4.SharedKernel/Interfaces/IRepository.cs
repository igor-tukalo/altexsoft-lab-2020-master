using System.Collections.Generic;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IRepository
    {
        T GetById<T>(int id) where T : BaseEntity;
        List<T> GetList<T>() where T : BaseEntity;
        void Add<T>(T entity) where T : BaseEntity;
        void Update<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
    }
}
