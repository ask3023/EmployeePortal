using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class LogIdentifier : ILogIdentifier
    {
        public LogIdentifier() : this(Guid.NewGuid())
        {
        }

        public LogIdentifier(Guid logId)
        {
            LogId = logId;
        }

        public Guid LogId
        {
            get; private set;
        }
    }
}
