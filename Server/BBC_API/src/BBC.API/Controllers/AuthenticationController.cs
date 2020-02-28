using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Identity.Auth;
using BBC.Services.Identity.Dto;
using BBC.Services.Types.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAction([FromBody] LoginDto model)
        {
            ResponseBase result = await _authService.Login(model.Email, model.Password);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest model)
        {
            ResponseBase result = await _authService.Register(model, model.Password);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest model)
        {
            ResponseBase result = await _authService.RefreshToken(model);
            return Ok(result);
        }


        [HttpGet("Logout")]
        [Authorize]
        public async Task Logout()
        {
            await Task.FromResult(0);
        }
    }
}