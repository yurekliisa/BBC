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

namespace BBC.API.Controllers
{
    [Authorize]
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
        [ProducesResponseType(typeof(IEnumerable<IdentityUser>), 200)]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IdentityUser), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            if (Id == 0)
                return BadRequest(new string[] { "Empty parameter!" });
            var user = _userService.GetUser(Id);

            if (user == null)
            {
                return BadRequest(new string[] { "User not found!" });
            }

            return Ok(user);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("InsertWithRole")]
        public async Task<IActionResult> Post([FromBody]CreateUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            IdentityResult result = await _userService.CreateUser(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
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
                return Ok(result);
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
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}
