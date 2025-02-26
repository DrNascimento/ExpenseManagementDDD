using Application.Interfaces;
using Application.ViewModel.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expenses")]
public class ExpenseController(IExpenseAppService expenseAppService) : ApiController
{
    private readonly IExpenseAppService _expenseAppService = expenseAppService;

    [HttpPost]
    public async Task<IActionResult> Post(CreateExpenseViewModel createExpenseViewModel)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _expenseAppService.Create(createExpenseViewModel);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ExpenseViewModel), 200)]
    public async Task<IActionResult> Get(Guid id)
    {
        var expenseViewModel = await _expenseAppService.Get(id);

        return NotFoundIfNull(expenseViewModel);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseViewModel>), 200)]
    public IActionResult GetAll()
    {
        var expenseViewModels = _expenseAppService.GetAll();
        return Ok(expenseViewModels);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(Guid id, UpdateExpenseViewModel updateExpenseViewModel)
    {
        await _expenseAppService.Update(id, updateExpenseViewModel);
        return Ok();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _expenseAppService.Delete(id);
        return NoContent();
    }
}
