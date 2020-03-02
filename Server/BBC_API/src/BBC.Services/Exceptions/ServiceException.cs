using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Exceptions
{
    public class ServiceException:Exception
    {
        public ServiceException(string message):base(message)
        {

        }
    }
}
