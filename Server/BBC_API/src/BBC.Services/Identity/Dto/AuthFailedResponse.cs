using BBC.Services.Types.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    public class AuthFailedResponse:ResponseBase
    {
        public IEnumerable<string> Errors { get; set; }

    }
}
