using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpPortal.Api.Infrastructure
{
    public interface ILogIdentifier
    {
        Guid LogId { get; }

        void SetLogId(Guid logId);
    }
}
