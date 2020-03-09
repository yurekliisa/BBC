using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.MediaService;
using BBC.Services.Services.MediaService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<MediaListDto>), 200)]
        [Route("GetData")]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await _mediaService.GetAllMedia();
            return Ok(result);
        }
    }
}