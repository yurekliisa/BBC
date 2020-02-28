using AutoMapper;
using BBC.Core.Domain.Identity;

namespace BBC.Services.Identity.Dto
{
    [AutoMap(typeof(User))]
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
