using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.SocialMediaService;
using BBC.Services.Services.SocialMediaService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SocialMediaListDto>), 200)]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            var socialmedias = await _socialMediaService.GetAllSocialMedias();
            return Ok(socialmedias);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Task<SocialMediaListDto>), 200)]
        [Route("Get")]
        public IActionResult Get(int Id) 
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var socialMedia = _socialMediaService.GetSocialMedia(Id);

            if (socialMedia == null)
            {
                return BadRequest();
            }
            return Ok(socialMedia);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateSocialMediaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _socialMediaService.CreateSocialMedia(model);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] EditSocialMediaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            await _socialMediaService.EditSocialMedia(model);
            return Ok();

        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            await _socialMediaService.DeleteSocialMedia(Id);
            return Ok();
        }
    }
}