using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.HomeService;
using BBC.Services.Services.HomeService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        public HomeController(
            IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<SliderOutputDto>), 200)]
        [Route("GetSliderImages")]
        public async Task<IActionResult> GetSliderImages()
        {
            var result = await _homeService.GetSliderImages();
            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<TaRHomeOuputDto>), 200)]
        [Route("GetHomeContent")]
        public async Task<IActionResult> GetHomeContent(int page)
        {
            if (page <= 0)
                return BadRequest("Sayfa Sayısı 0 ve 0'dan küçük olamaz");

            var result = await _homeService.GetHomeContent(page);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<PopularTaROutputDto>), 200)]
        [Route("PopularTaRByCategory")]
        public async Task<IActionResult> PopularTaRByCategory()
        {
            var result = await _homeService.GetTaRByPopularCategory();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PopularTaROutputDto>), 200)]
        [Route("PopularTaRByForYou")]
        public async Task<IActionResult> PopularTaRByForYou()
        {
            var result = await _homeService.GetTaRByForYou();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PopularChefOutputDto>), 200)]
        [Route("GetPopularChefs")]
        public async Task<IActionResult> GetPopularChefs()
        {
            var result = await _homeService.GetPopularChefs();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PopularCategory>), 200)]
        [Route("GetPopularCategories")]
        public async Task<IActionResult> GetPopularCategories()
        {
            var result = await _homeService.GetPopularCategories();
            return Ok(result);
        }
    }
}