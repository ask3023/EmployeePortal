using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Common.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly ILogger _logger;
        public GlobalExceptionHandler(ILoggerFactory loggerFactory)
        {
            if(loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }

            _logger = loggerFactory.CreateLogger<GlobalExceptionHandler>();
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Global handler: " + context.Exception.ToString());
        }
    }
}
