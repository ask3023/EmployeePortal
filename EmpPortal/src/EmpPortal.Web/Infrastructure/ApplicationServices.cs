using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class ApplicationServices
    {
        public static ILoggerFactory LoggerFactory { get; set; }

        public static IConfiguration Configuration { get; set; }
    }
}
