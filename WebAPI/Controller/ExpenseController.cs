using Application.Interfaces;
using Application.ViewModel.Expense;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ApiController
    {
        private readonly IExpenseAppService _expenseAppService;

        public ExpenseController(IExpenseAppService expenseAppService) 
        {
            _expenseAppService = expenseAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateExpenseViewModel createExpenseViewModel)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _expenseAppService.Create(createExpenseViewModel);
            return Ok(new { id });
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var expenseViewModel = _expenseAppService.Get(id);

            return OkFind(expenseViewModel);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expenseViewModels = _expenseAppService.GetAll();
            return Ok(expenseViewModels);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, UpdateExpenseViewModel updateExpenseViewModel)
        {
            await _expenseAppService.Update(id, updateExpenseViewModel);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _expenseAppService.Delete(id);
            return Ok();
        }
    }
}
