using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BBC.Core.Domain.Identity;
using BBC.Services.Identity.Dto.UserDtos;

namespace BBC.API.Controllers
{
    [Produces("application/json")]
    [Route("api/UserRoles")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRolesController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [Route("Get/{Id}")]
        public async Task<IActionResult> Get(string Id)
        {
            User user = await _userManager.FindByIdAsync(Id);
            return Ok(await _userManager.GetRolesAsync(user));
        }
      
        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Add")]
        public async Task<IActionResult> Post([FromBody]UserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            User user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            Role role = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
            if (role == null)
                return BadRequest(new string[] { "Could not find role!" });

            IdentityResult result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }

        /// <summary>
        /// Delete a user from an existing role
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Delete/{Id}/{RoleId}")]
        public async Task<IActionResult> Delete(string Id, string RoleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            User user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return BadRequest(new string[] { "Could not find user!" });

            Role role = await _roleManager.FindByIdAsync(RoleId);
            if (user == null)
                return BadRequest(new string[] { "Could not find role!" });

            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}
