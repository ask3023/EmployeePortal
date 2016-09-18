using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Api.Infrastructure
{
    public class ApplicationServices
    {
        public static ILoggerFactory LoggerFactory { get; set; }

        public static IConfiguration Configuration { get; set; }

        public static IContainer Container { get; set; } 
    }
}
