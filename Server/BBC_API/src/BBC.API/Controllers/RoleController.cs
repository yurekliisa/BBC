using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Identity.Dto.RoleDtos;

namespace BBC.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _roleService.GetRoles();
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(IdentityResult), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [Route("Create")]
        public async Task<IActionResult> Post([FromBody]CreateRoleDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            IdentityResult result = await _roleService.CreateRole(model);
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
        public async Task<IActionResult> Put(int Id, [FromBody]EditRoleDto model)
        {
            IdentityResult result = await _roleService.EditRole(Id, model);
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
            if (String.IsNullOrEmpty(Id))
                return BadRequest(new string[] { "Could not complete request!" });

            IdentityResult result = await _roleService.Delete(Id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}