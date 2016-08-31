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
        public ILogger Logger;

        public BaseController()
        {
            var loggerFactory = ApplicationLogger.LoggerFactory;
            Logger = loggerFactory.CreateLogger(this.GetType());
        }
    }
}
