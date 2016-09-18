using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class LogIdMiddleware
    {
        private RequestDelegate _next;
        public LogIdMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public Task Invoke(HttpContext context)
        {
            // TODO: do all the transaction identifier job

            return _next(context);
        }
    }
}
