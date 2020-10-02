using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System;

namespace HomeTask4.Core.Controllers
{
    public abstract class BaseController
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IOptions<CustomSettings> CustomSettingsApp { get; }

        protected BaseController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            CustomSettingsApp = settings;
        }
    }
}
