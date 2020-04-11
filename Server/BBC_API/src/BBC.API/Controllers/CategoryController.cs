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

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto input)
        {
            await _categoryService.CreateCategories(input);
            return Ok();
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditCategoryDto input)
        {
            await _categoryService.EditCategories(input);
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryListDto>), 200)]
        [Route("GetAllCategories")]
        //Admine özel
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }


    }
}