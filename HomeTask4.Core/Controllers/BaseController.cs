﻿using HomeTask4.SharedKernel;
using HomeTask4.SharedKernel.Interfaces;
using System;

namespace HomeTask4.Core.Controllers
{
    public abstract class BaseController
    {
        protected Validation ValidManager { get; }
        protected IUnitOfWork UnitOfWork { get; }

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            ValidManager = new Validation();
        }
    }
}
