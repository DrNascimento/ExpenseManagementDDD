using Application.DTO.Input.Expense;
using Application.DTO.Output.Expense;
using Application.Handlers.Expense;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expenses")]
public class ExpenseController(IUserContext userContext) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Post(
        CreateExpenseInput input,
        [FromServices] CreateExpenseHandler createExpenseHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await createExpenseHandler.ExecuteAsync(input, userContext.UserId);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ExpenseOutput), 200)]
    public async Task<IActionResult> Get(
        Guid id,
        [FromServices] GetExpenseHandler getExpenseHandler)
    {
        var expense = await getExpenseHandler.ExecuteAsync(id);

        return NotFoundIfNull(expense);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseOutput>), 200)]
    public IActionResult GetAll(
        [FromServices] GetAllExpensesHandler getAllExpensesHandler)
    {
        var expenses = getAllExpensesHandler.Execute(userContext.UserId);
        return Ok(expenses);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(
        Guid id,
        UpdateExpenseInput input,
        [FromServices] UpdateExpenseHandler updateExpenseHandler)
    {
        await updateExpenseHandler.ExecuteAsync(id, input, userContext.UserId);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteExpenseHandler deleteExpenseHandler)
    {
        await deleteExpenseHandler.ExecuteAsync(id, userContext.UserId);
        return NoContent();
    }
}
