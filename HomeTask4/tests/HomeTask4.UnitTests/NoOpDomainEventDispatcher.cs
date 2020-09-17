using System.Threading.Tasks;
using HomeTask4.SharedKernel.Interfaces;
using HomeTask4.SharedKernel;

namespace HomeTask4.UnitTests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public Task Dispatch(BaseDomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
