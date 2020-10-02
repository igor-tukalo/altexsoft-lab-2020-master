using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace HomeTask4.Core.Controllers
{
    public abstract class BaseController
    {
        protected ValidationNavigation ValidManager { get; }
        protected IUnitOfWork UnitOfWork { get; }
        protected IOptions<CustomSettings> CustomSettingsApp { get; }

        protected BaseController(IUnitOfWork unitOfWork, IOptions<CustomSettings> settings)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            CustomSettingsApp = settings;
            ValidManager = new ValidationNavigation();
        }
    }
}
