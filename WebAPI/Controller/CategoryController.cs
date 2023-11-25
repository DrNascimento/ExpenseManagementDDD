using Application.Interfaces;
using Application.ViewModel.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ApiController
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService) 
        {
            _categoryAppService = categoryAppService;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CreateCategoryViewModel), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryAppService.Get(id);

            return OkFind(category);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreateCategoryViewModel>), 200)]
        public IActionResult GetAll()
        {
            var categories = _categoryAppService.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post(CreateCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _categoryAppService.CreateByUser(categoryViewModel);
            return Ok(new { id });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("universal")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PostAdmin(CreateCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _categoryAppService.CreateUniversal(categoryViewModel);
            return Ok(new { id });
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCategoryViewModel updateCategoryViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _categoryAppService.Update(id, updateCategoryViewModel);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryAppService.Delete(id);
            return Ok();
        }
    }
}
