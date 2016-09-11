using Autofac;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            var loggerFactory = ApplicationServices.LoggerFactory;

            var accessor = ApplicationServices.Container.Resolve<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            var resolved = (ILogIdentifier)context.RequestServices.GetService(typeof(ILogIdentifier));

            Logger = new EmpLogger(
                    loggerFactory.CreateLogger(this.GetType()),
                    resolved
                    );

            HttpHelper = new HttpHelper();
        }

        public HttpHelper HttpHelper { get; private set; }
        public IEmpLogger Logger { get; private set; }
    }
}
