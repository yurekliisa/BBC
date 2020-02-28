using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BBC.Services.Types.Response
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public HttpStatusCode ResultCode { get; set; }
    }
}
