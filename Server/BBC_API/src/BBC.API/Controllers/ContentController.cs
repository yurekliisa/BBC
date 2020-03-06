using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.ContentService;
using BBC.Services.Services.ContentService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ContentListDto>), 200)]
        [Route("GetData")]
        public async Task<IActionResult> GetAllContents()
        {
            return Ok(await _contentService.GetAllContents());
        }
    }
}