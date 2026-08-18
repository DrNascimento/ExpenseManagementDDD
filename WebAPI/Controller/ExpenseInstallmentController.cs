using Application.DTO.Input.ExpenseInstallment;
using Application.DTO.Output.ExpenseInstallment;
using Application.Handlers.ExpenseInstallment;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expenses-installments")]
public class ExpenseInstallmentController(IUserContext userContext) : ApiController
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(
        Guid id,
        [FromServices] GetExpenseInstallmentHandler getExpenseInstallmentHandler)
    {
        ExpenseInstallmentOutput expenseInstallment = await getExpenseInstallmentHandler.ExecuteAsync(id, userContext.UserId);

        return NotFoundIfNull(expenseInstallment);
    }

    [HttpGet("date/{year:int}/{month:int}/{day:int}")]
    public IActionResult GetByDate(
        int year,
        int month,
        int day,
        [FromServices] GetExpenseInstallmentsByDateHandler getExpenseInstallmentsByDateHandler)
    {
        IEnumerable<ExpenseInstallmentOutput> expenseInstallments = getExpenseInstallmentsByDateHandler.Execute(year, month, day, userContext.UserId);
        return Ok(expenseInstallments);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(Guid id,
        UpdateExpenseInstallmentInput input,
        [FromServices] UpdateExpenseInstallmentHandler updateExpenseInstallmentHandler)
    {
        await updateExpenseInstallmentHandler.ExecuteAsync(id, input, userContext.UserId);
        return Ok();
    }

    [HttpPut("paid/{id:Guid}")]
    public async Task<IActionResult> Put(
        Guid id,
        [FromServices] TogglePaidExpenseInstallmentHandler togglePaidExpenseInstallmentHandler)
    {
        await togglePaidExpenseInstallmentHandler.ExecuteAsync(id, userContext.UserId);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteExpenseInstallmentHandler deleteExpenseInstallmentHandler)
    {
        await deleteExpenseInstallmentHandler.ExecuteAsync(id, userContext.UserId);
        return NoContent();
    }
}
