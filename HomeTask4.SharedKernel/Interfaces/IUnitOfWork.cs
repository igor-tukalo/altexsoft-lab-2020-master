namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository Repository { get; }
        void SaveChanges();
    }
}
