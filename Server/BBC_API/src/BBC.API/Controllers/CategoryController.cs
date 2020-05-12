using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBC.API.Helper.Attribute;
using BBC.Services.Services.CategoryService;
using BBC.Services.Services.CategoryService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BBC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredAuth]
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
        [ProducesResponseType(200)]
        [Route("Create")]
        [RequiredAuth]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto input)
        {
            await _categoryService.CreateCategories(input);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [Route("Edit")]
        [RequiredAuth]
        public async Task<IActionResult> Edit([FromBody] EditCategoryDto input)
        {
            await _categoryService.EditCategories(input);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Route("Delete")]
        [RequiredAuth]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryListDto>), 200)]
        [Route("GetAllCategories")]
        [RequiredAuth]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }


    }
}