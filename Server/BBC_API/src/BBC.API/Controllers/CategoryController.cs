using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.Services.Services.CategoryService;
using BBC.Services.Services.CategoryService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(
            ICategoryService categoryService
            )
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), 200)]
        [Route("GetAllCategories")]
        //Admine özel
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }


    }
}