using System.Threading.Tasks;
using HomeTask4.SharedKernel;

namespace HomeTask4.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(BaseDomainEvent domainEvent);
    }
}