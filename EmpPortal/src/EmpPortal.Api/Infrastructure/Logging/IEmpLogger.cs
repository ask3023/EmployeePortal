using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Api.Infrastructure
{
    public interface IEmpLogger
    {
        ILogIdentifier LogIdentifier { get; }
        ILogger Logger { get; }

        void LogInformation(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogDebug(string message);
    }
}
