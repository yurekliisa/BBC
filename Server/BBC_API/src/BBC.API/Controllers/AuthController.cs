using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BBC.Infrastructure.Identity.Providers;
using Microsoft.Extensions.Options;
using BBC.Core.Configuration;
using BBC.API.Helper;
using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Identity.Dto.UserDtos;

namespace BBC.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(
           IAuthService authService
        )
        {
            _authService = authService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmailInputDto model)
        {
            if (model.UserId == null || model.Code == null)
            {
                return BadRequest(new string[] { "Error retrieving information!" });
            }

            var result = await _authService.ConfirmEmail(model);
            if (result.Succeeded)
                return Ok(result);

            return BadRequest(result.Errors.Select(x => x.Description));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var result = await _authService.Register(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(typeof(TokenOutputDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginInputDto model)
        {
            var result = await _authService.CreateToken(model);
            if (result.Errors != null)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var result = await _authService.ForgotPassword(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            var result = await _authService.ResetPassword(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ResendVerificationEmail")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody]UserDto model)
        {
            var result = await _authService.ResendVerificationEmail(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }

    }
}