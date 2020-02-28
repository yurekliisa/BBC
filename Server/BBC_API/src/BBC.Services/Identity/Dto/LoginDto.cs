using AutoMapper;
using BBC.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Identity.Dto
{
    [AutoMap(typeof(User))]
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
