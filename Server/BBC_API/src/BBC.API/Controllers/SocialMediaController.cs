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

        //Eylül: Buraya tekrar bakılacak!
        //String değer almak daha mı doğru olur?
        [HttpGet]
        public async Task<IActionResult> GetAllSocialMedia()
        {
            var socialmedias = await _socialMediaService.GetAllSocialMedias();
            return Ok(socialmedias);
        }
        [HttpGet]
        public IActionResult Get(int Id) //string Url??
        {
            if(Id == 0)
            {
                return BadRequest();
            }

            var socialMedia = _socialMediaService.GetSocialMedia(Id);

            if(socialMedia == null)
            {
                return BadRequest();
            }
            return Ok(socialMedia);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSocialMediaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            IdentityResult result = await _socialMediaService.CreateSocialMedia(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
        [HttpPut]
        public async Task<IActionResult> Put(string Url, [FromBody] EditSocialMediaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.FirstOrDefault().ErrorMessage));

            IdentityResult result = await _socialMediaService.EditSocialMedia(Url, model);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string Url)
        {
            if (!String.IsNullOrEmpty(Url))
                return BadRequest(new string[] { "Empty parameter!" });

            IdentityResult result = await _socialMediaService.DeleteSocialMedia(Url);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors.Select(x => x.Description));
        }
    }
}