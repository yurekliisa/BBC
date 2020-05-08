using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using BBC.Infrastructure.Identity.Providers;
using BBC.Core.Configuration;
using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.Auth;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.API.Helper.Attribute;

namespace BBC.API.Controllers
{
    [RequiredAuth]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManageController : Controller
    {
        private readonly IManageService _manageService;

        public ManageController(
            IManageService manageService
            )
        {
            _manageService = manageService;

        }


        [HttpGet]
        [ProducesResponseType(typeof(UserInfoOutputDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _manageService.UserInfo();
            if (user == null)
            {
                return BadRequest(new string[] { "Could not find user!" });
            }

            return Ok(user);
        }




        [HttpGet]
        [ProducesResponseType(typeof(EnableAuthenticatorInputDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("EnableAuthenticator")]
        public async Task<IActionResult> EnableAuthenticator()
        {
            var result = await _manageService.EnableAuthenticator();
            if (result == null)
                return BadRequest(new string[] { "Could not find user!" });

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            IdentityResult result = await _manageService.ChangePassword(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("SendVerificationEmail")]
        public async Task<IActionResult> SendVerificationEmail()
        {
            IdentityResult result = await _manageService.SendVerificationEmail();
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("SetPassword")]
        public async Task<IActionResult> SetPassword([FromBody]SetPasswordInputDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityResult result = await _manageService.SetPassword(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("EnableAuthenticator")]
        public async Task<IActionResult> EnableAuthenticator([FromBody]EnableAuthenticatorInputDto model)
        {
            var result = await _manageService.EnableAuthenticator(model, ModelState.IsValid);
            if (!result.IdentityResult.Succeeded)
            {
                if (result.isView)
                {
                    return View(result.EnableAuthenticatorDto);
                }
                return BadRequest(result.IdentityResult.Errors.Select(x => x.Description));
            }
            return Ok(result);


        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("ResetAuthenticator")]
        public async Task<IActionResult> ResetAuthenticator()
        {
            IdentityResult result = await _manageService.ResetAuthenticator();
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpPost]
        [ProducesResponseType(typeof(ShowRecoveryCodesOutputDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("GenerateRecoveryCodes")]
        public async Task<IActionResult> GenerateRecoveryCodes()
        {
            ShowRecoveryCodesOutputDto result = await _manageService.GenerateRecoveryCodes();
            if (result.IdentityResult.Succeeded)
            {
                return Ok(result.RecoveryCodes);
            }
            return BadRequest(result.IdentityResult.Errors.Select(x => x.Description));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenOutputDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("UpdateToken")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateToken([FromBody]TokenInputDto model)
        {
            var result = await _manageService.RefreshToken(model);
            if (result.Errors != null)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
