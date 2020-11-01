using HomeTask4.SharedKernel.Interfaces;
using System;

namespace HomeTask4.Core.Controllers
{
    public abstract class BaseController
    {
        protected IUnitOfWork UnitOfWork { get; }

        protected BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
    }
}
