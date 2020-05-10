using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Identity.Interfaces;
using BBC.Services.Services.CategoryService;
using BBC.Services.Services.CategoryService.Dto;
using BBC.Services.Services.TarifAndReceteService;
using BBC.Services.Services.TarifAndReceteService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaRController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITarifAndReceteService _tarifAndReceteService;
        private readonly ICategoryService _categoryService;
        public TaRController(
            ITarifAndReceteService tarifAndReceteService,
            ICategoryService categoryService,
            IUserService userService
            )
        {
            _userService = userService;
            _tarifAndReceteService = tarifAndReceteService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryListDto>), 200)]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TarifAndReceteListDto>), 200)]
        [Route("GetTarifAndReceteByUserId")]
        public async Task<IActionResult> GetTarifAndReceteByUserId(int userId)
        {
            var users = await _userService.GetUser(userId);
            var tarifAndRecete = await _tarifAndReceteService.GetTarifAndRecete(Convert.ToInt32(users.Id));
            return Ok(tarifAndRecete);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [Route("CreateTaR")]
        public async Task<IActionResult> CreateTaR([FromBody] CreateTarifAndReceteDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.Select(x => x.Errors.SelectMany(y => y.ErrorMessage)));

            var newId = await _tarifAndReceteService.CreateTaR(input);
            return Ok(newId);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CreateTarifAndReceteOutputDto), 200)]
        [Route("Create")]
        //Kullanıcının giriş yapması yeterli 
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
        //Kullanıcının giriş yapması yeterli 
        public async Task<IActionResult> GetAllTarifAndRecetes()
        {
            var result = await _tarifAndReceteService.GetAllTarifAndRecetes();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(TarifAndReceteDetailListDto), 200)]
        [Route("GetAllTarifAndReceteDetails")]
        //Kullanıcının giriş yapması yeterli 
        public async Task<IActionResult> GetAllTarifAndReceteDetails()
        {
            var result = await _tarifAndReceteService.GetAllTarifAndReceteDetails();
            return Ok(result);
        }
    }
}