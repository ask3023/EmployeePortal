using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MVCApp.Infrastructure
{
    public class HttpResponse<T>
    {
        public T ResponseModel { get; set; }

        public bool IsRequestSuccess { get; set; } = true;

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
