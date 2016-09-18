using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    /// <summary>
    /// Use this exception to notify any known business exceptions
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Error code indicates type of exception within the application
        /// Define range of error codes for various type of exceptions
        /// </summary>
        public int ErrorCode { get; set; }
    }
}
