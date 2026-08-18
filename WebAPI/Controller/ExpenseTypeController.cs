using Application.DTO.Input.ExpenseType;
using Application.DTO.Output.ExpenseType;
using Application.Handlers.ExpenseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expense-types")]
public class ExpenseTypeController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseTypeOutput>), 200)]
    public IActionResult GetAll([FromServices] GetAllExpenseTypesHandler getAllExpenseTypesHandler)
    {
        IEnumerable<ExpenseTypeOutput> expenses = getAllExpenseTypesHandler.Execute();

        return Ok(expenses);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ExpenseTypeOutput), 200)]
    public async Task<IActionResult> Get(
        Guid id,
        [FromServices] GetExpenseTypeByIdHandler getExpenseTypeByIdHandler)
    {
        ExpenseTypeOutput expenseType = await getExpenseTypeByIdHandler.ExecuteAsync(id);
        return NotFoundIfNull(expenseType);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] CreateExpenseTypeInput input,
        [FromServices] CreateExpenseTypeHandler createExpenseTypeHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid id = await createExpenseTypeHandler.ExecuteAsync(input);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }
}
