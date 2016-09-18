using Autofac;
using EmpPortal.Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Api.Infrastructure
{
    public class BaseController : Controller
    {
        public IEmpLogger Logger;

        public BaseController()
        {
            var loggerFactory = ApplicationServices.LoggerFactory;
            var accessor = ApplicationServices.Container.Resolve<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            var logIdentifier = (ILogIdentifier)context.RequestServices.GetService(typeof(ILogIdentifier));

            Logger = new EmpLogger(
                    loggerFactory.CreateLogger(this.GetType()),
                    logIdentifier
                    );
        }
    }
}
