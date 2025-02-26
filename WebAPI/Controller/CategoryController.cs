using Application.Interfaces;
using Application.ViewModel.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryAppService categoryAppService) : ApiController
{
    private readonly ICategoryAppService _categoryAppService = categoryAppService;

    [HttpGet("{id:Guid}", Name = "Get")]
    [ProducesResponseType(typeof(CreateCategoryViewModel), 200)]
    public async Task<IActionResult> Get(Guid id)
    {
        var category = await _categoryAppService.Get(id);

        return NotFoundIfNull(category);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CreateCategoryViewModel>), 200)]
    public IActionResult GetAll()
    {
        var categories = _categoryAppService.GetAll();

        return Ok(categories);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Post(CreateCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _categoryAppService.CreateByUser(categoryViewModel);

        return Created(Url.Action(nameof(Get), new { id })!,  null);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("universal")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAdmin(CreateCategoryViewModel categoryViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _categoryAppService.CreateUniversal(categoryViewModel);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryViewModel updateCategoryViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _categoryAppService.Update(id, updateCategoryViewModel);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryAppService.Delete(id);
        return NoContent();
    }


    [HttpGet("summary")]
    [ProducesResponseType(typeof(CategoriesSummaryViewModel), 200)]
    public async Task<IActionResult> Summary([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        CategoriesSummaryViewModel categories = await _categoryAppService.Summary(start, end);

        return Ok(categories);
    }

}
