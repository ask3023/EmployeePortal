using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MVCApp.Infrastructure
{
    public class EmpLogger : IEmpLogger
    {

        public EmpLogger(ILogger logger, ILogIdentifier logIdentifier)
        {
            Logger = logger;
            LogIdentifier = logIdentifier;
        }

        public ILogger Logger
        {
            get; private set;
        }

        public ILogIdentifier LogIdentifier
        {
            get; private set;
        }

        public void LogDebug(string message)
        {
            Logger.LogDebug($" {LogIdentifier.LogId} | {message}");
        }

        public void LogError(string message)
        {
            Logger.LogError($" {LogIdentifier.LogId} | {message}");
        }

        public void LogInformation(string message)
        {
            Logger.LogInformation($" {LogIdentifier.LogId} | {message}");
        }

        public void LogWarning(string message)
        {
            Logger.LogWarning($" {LogIdentifier.LogId} | {message}");
        }
    }
}
