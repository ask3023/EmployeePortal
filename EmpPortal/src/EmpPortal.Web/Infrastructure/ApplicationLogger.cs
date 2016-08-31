using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class ApplicationLogger
    {
        public static ILoggerFactory LoggerFactory { get; set; }
    }
}
