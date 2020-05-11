using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Identity.Dto.UserDtos;
using BBC.Services.Services.CategoryService;
using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.TarifAndReceteService;
using BBC.Services.Services.TarifAndReceteService.Dto;
using BBC.Services.Identity.Dto.Auth;

namespace BBC.API.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserListDto>), 200)]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }


        [HttpGet]
        [ProducesResponseType(typeof(UserProfileDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Get/{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            if (Id == 0)
                return BadRequest(new string[] { "Empty parameter!" });
            var user = await _userService.GetUser(Id);

            if (user == null)
            {
                return BadRequest(new string[] { "User not found!" });
            }

            return Ok(user);
        }



        [HttpPut]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Update/{Id}")]
        public async Task<IActionResult> Put(string Id, [FromBody]EditUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            IdentityResult result = await _userService.EditUser(Id, model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
                return BadRequest(new string[] { "Empty parameter!" });

            IdentityResult result = await _userService.Delete(Id);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}
