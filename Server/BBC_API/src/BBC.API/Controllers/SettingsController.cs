using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.SettingsService;
using BBC.Services.Services.SettingsService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SettingsListDto>), 200)]
        [Route("GetSettings")]
        public async Task<IActionResult> GetAllSettings()
        {
            var result = await _settingsService.GetAllSettings();
            return Ok(result);
        }
    }
}