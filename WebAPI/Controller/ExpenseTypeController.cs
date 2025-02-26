using Application.Interfaces;
using Application.ViewModel.ExpenseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/expense-types")]
public class ExpenseTypeController(IExpenseTypeAppService expenseTypeAppService) : ApiController
{
    private readonly IExpenseTypeAppService _expenseTypeAppService = expenseTypeAppService;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ExpenseTypeViewModel>), 200)]
    public IActionResult GetAll()
    {
        IEnumerable<ExpenseTypeViewModel> expenses = _expenseTypeAppService.GetAll();

        return Ok(expenses);
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(ExpenseTypeViewModel), 200)]
    public async Task<IActionResult> Get(Guid id)
    {
        ExpenseTypeViewModel expenseTypeViewModel = await _expenseTypeAppService.GetById(id);
        return NotFoundIfNull(expenseTypeViewModel);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ExpenseTypeViewModel expenseTypeViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid id = await _expenseTypeAppService.Create(expenseTypeViewModel);
        return Created(Url.Action(nameof(Get), new { id })!, null);
    }

    
}
