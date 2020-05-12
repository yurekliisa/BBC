using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.API.Helper.Attribute;
using BBC.API.Registery;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.CategoryService;
using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.HomeService.Dto;
using BBC.Services.Services.TarifAndReceteService;
using BBC.Services.Services.TarifAndReceteService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaRController : ControllerBase
    {
        private readonly ITarifAndReceteService _tarifAndReceteService;
        private readonly ICategoryService _categoryService;
        public TaRController(
            ITarifAndReceteService tarifAndReceteService,
            ICategoryService categoryService
            )
        {
            _tarifAndReceteService = tarifAndReceteService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TaRHomeOuputDto>), 200)]
        [Route("GetTaR")]
        public async Task<IActionResult> GetTaR(int page)
        {
            if (page <= 0)
                return BadRequest("Sayfa Sayısı 0 ve 0'dan küçük olamaz");

            var result = await _tarifAndReceteService.GetTarifAndRecetes(page);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<UserTarifAndReceteDto>), 200)]
        [Route("GetTarifAndReceteByUserId")]
        public async Task<IActionResult> GetTarifAndReceteByUserId(int userId)
        {
            var tarifAndRecete = await _tarifAndReceteService.GetTarifAndReceteByUserId(userId);
            return Ok(tarifAndRecete);
        }
       
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [Route("CreateTaR")]
        [RequiredAuth]
        public async Task<IActionResult> CreateTaR([FromForm] CreateTarifAndReceteDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.SelectMany(y => y.ErrorMessage)));

            var newId = await _tarifAndReceteService.CreateTaR(input);
            return Ok(newId);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CreateTarifAndReceteOutputDto), 200)]
        [Route("Create")]
        [RequiredAuth]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategories();
            var result = new CreateTarifAndReceteOutputDto()
            {
                Categories = categories
            };
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TarifAndReceteListDto), 200)]
        [Route("GetAllTarifAndRecetes")]
        public async Task<IActionResult> GetAllTarifAndRecetes(int page)
        {
            if (page <= 0)
            {
                return BadRequest("Sayfa sayısı 0 ve 0'dan küçük olamaz");
            }
            var result = await _tarifAndReceteService.GetAllTarifAndRecetes(page);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TarifAndReceteDetailDto), 200)]
        [Route("GetTarifAndReceteDetails")]
        public async Task<IActionResult> GetTarifAndReceteDetails(int tarId)
        {
            var result = await _tarifAndReceteService.GetTarifAndReceteDetails(tarId);
            return Ok(result);
        }

        [HttpGet]
        [Route("Delete")]
        [RequiredAuth]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!String.IsNullOrEmpty(Id.ToString()))
            {
                return BadRequest();
            }
            await _tarifAndReceteService.DeleteTarifAndRecete(Id);
            return Ok();
        }

        [HttpPost]
        [Route("Edit")]
        [RequiredAuth]
        public async Task<IActionResult> Edit([FromBody] UserTarifAndReceteDto input)
        {
            await _tarifAndReceteService.EditTarifAndRecete(input);
            return Ok();
        }
       
        [HttpPost]
        [Route("Comments")]
        [RequiredAuth]
        public async Task<IActionResult> Comment([FromBody] CommentDto input)
        {
            await _tarifAndReceteService.Comment(input);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GelAllComment(int tarId)
        {
            var result = await _tarifAndReceteService.GetAllComments(tarId);
            return Ok(result);
        }
    }
}