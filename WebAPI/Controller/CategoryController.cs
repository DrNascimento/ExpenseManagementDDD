using Application.Interfaces;
using Application.ViewModel.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService) 
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet("{int:id}")]
        [ProducesResponseType(typeof(CategoryViewModel), 200)]

        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryAppService.Get(id);
            return category is null ? NotFound() : Ok(category);
        }
    }
}
