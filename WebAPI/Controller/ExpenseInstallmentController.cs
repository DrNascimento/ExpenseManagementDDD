using Application.Interfaces;
using Application.ViewModel.ExpenseInstallment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expenses-installments")]
public class ExpenseInstallmentController(IExpenseInstallmentAppService expenseInstallmentAppService) : ApiController
{
    private readonly IExpenseInstallmentAppService _expenseInstallmentAppService = expenseInstallmentAppService;

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        ExpenseInstallmentViewModel expenseInstallment = await _expenseInstallmentAppService.Get(id);

        return NotFoundIfNull(expenseInstallment);
    }

    [HttpGet("date/{year:int}/{month:int}/{day:int}")]
    public IActionResult GetByDate(int year, int month, int day)
    {
        IEnumerable<ExpenseInstallmentViewModel> expenseInstallments = _expenseInstallmentAppService.GetByDate(year, month, day);
        return Ok(expenseInstallments);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(Guid id,
        UpdateExpenseInstallmentViewModel updateExpenseInstallmentViewModel)
    {
        await _expenseInstallmentAppService.Update(id, updateExpenseInstallmentViewModel);
        return Ok();
    }

    [HttpPut("paid/{id:Guid}")]
    public async Task<IActionResult> Put(Guid id)
    {
        await _expenseInstallmentAppService.UpdatePaid(id);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _expenseInstallmentAppService.Delete(id);
        return NoContent();
    }
}
