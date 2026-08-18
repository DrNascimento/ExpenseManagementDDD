using Application.DTO.Input.Category;
using Application.DTO.Output.Category;
using Application.Handlers.Category;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController(IUserContext userContext) : ApiController
{
    [HttpGet("{id:Guid}", Name = "Get")]
    [ProducesResponseType(typeof(CategoryOutput), 200)]
    public async Task<IActionResult> Get(
        Guid id,
        [FromServices] GetCategoryHandler getCategoryHandler)
    {
        var category = await getCategoryHandler.ExecuteAsync(id);

        return NotFoundIfNull(category);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryOutput>), 200)]
    public IActionResult GetAll([FromServices] GetAllCategoriesHandler getAllCategoriesHandler)
    {
        var categories = getAllCategoriesHandler.Execute(userContext.UserId);

        return Ok(categories);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Post(
        CreateCategoryInput input,
        [FromServices] CreateCategoryByUserHandler createCategoryByUserHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await createCategoryByUserHandler.ExecuteAsync(input, userContext.UserId);

        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    [Authorize(Roles = "admin")]
    [HttpPost("universal")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> PostAdmin(
        CreateCategoryInput input,
        [FromServices] CreateCategoryUniversalHandler createCategoryUniversalHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await createCategoryUniversalHandler.ExecuteAsync(input);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put(
        Guid id,
        [FromBody] UpdateCategoryInput input,
        [FromServices] UpdateCategoryHandler updateCategoryHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await updateCategoryHandler.ExecuteAsync(id, input, userContext.UserId, userContext.Role);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteCategoryHandler deleteCategoryHandler)
    {
        await deleteCategoryHandler.ExecuteAsync(id, userContext.UserId, userContext.Role);
        return NoContent();
    }

    [HttpGet("summary")]
    [ProducesResponseType(typeof(CategoriesSummaryOutput), 200)]
    public async Task<IActionResult> Summary(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end,
        [FromServices] GetCategorySummaryHandler getCategorySummaryHandler)
    {
        CategoriesSummaryOutput categories = await getCategorySummaryHandler.ExecuteAsync(start, end, userContext.UserId);

        return Ok(categories);
    }
}
