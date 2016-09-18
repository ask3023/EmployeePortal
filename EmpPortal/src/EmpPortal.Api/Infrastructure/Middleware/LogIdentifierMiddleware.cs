using EmpPortal.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Api.Infrastructure
{
    public class LogIdentifierMiddleware
    {
        RequestDelegate _next;

        public LogIdentifierMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            // TODO: do all the transaction identifier job
            var logId = context.Request.Headers["X-LogIdentifier"];

            if(!string.IsNullOrWhiteSpace(logId))
            {
                var logIdentifier = (ILogIdentifier)context.RequestServices.GetService(typeof(ILogIdentifier));
                logIdentifier.SetLogId(new Guid(logId));
            }

            return _next(context);
        }
    }

    public static class LogIdentifierExtensions
    {
        public static void UseLogIdentifier(this IApplicationBuilder app)
        {
            app.UseMiddleware<LogIdentifierMiddleware>();
        }
    }
}
