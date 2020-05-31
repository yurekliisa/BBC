using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.API.Helper.Attribute;
using BBC.Services.Services.LobiService;
using BBC.Services.Services.LobiService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredAuth]
    public class LobiController : ControllerBase
    {
        private readonly ILobiService _lobiService;
        public LobiController(ILobiService lobiService)
        {
            _lobiService = lobiService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("Create")]
        [RequiredAuth]
        public async Task<IActionResult> Create([FromBody] CreateLobiDto input)
        {
            await _lobiService.CreateLobi(input);
            return Ok();
        }

        [HttpGet]
        [Route("GetLobi")]
        [RequiredAuth]
        public async Task<IActionResult> GetLobi(int Id)
        {
            await _lobiService.GetLobi(Id);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<LobiListDto>), 200)]
        [Route("GetAllLobies")]
        [RequiredAuth]
        public async Task<IActionResult> GetAllLobies()
        {
            var lobies = await _lobiService.GetAllLobies();
            return Ok(lobies);
        }
    }
}