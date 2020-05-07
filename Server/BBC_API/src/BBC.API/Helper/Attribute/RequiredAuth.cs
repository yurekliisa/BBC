using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API.Helper.Attribute
{
    public class RequiredAuth : TypeFilterAttribute
    {
        public RequiredAuth() : base(typeof(AuthFilter))
        {
        }
    }
}
