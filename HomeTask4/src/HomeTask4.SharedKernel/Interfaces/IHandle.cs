using System.Threading.Tasks;
using HomeTask4.SharedKernel;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IHandle<in T> where T : BaseDomainEvent
    {
        Task Handle(T domainEvent);
    }
}