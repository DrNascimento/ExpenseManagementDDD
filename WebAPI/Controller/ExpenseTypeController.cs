using Application.Interfaces;
using Application.ViewModel.ExpenseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/expense-types")]
    public class ExpenseTypeController : ApiController
    {
        private readonly IExpenseTypeAppService _expenseTypeAppService;

        public ExpenseTypeController(IExpenseTypeAppService expenseTypeAppService)
        {
            _expenseTypeAppService = expenseTypeAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expenses = _expenseTypeAppService.GetAll();

            return Ok(expenses);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var expenseTypeViewModel = await _expenseTypeAppService.GetById(id);
            return OkFind(expenseTypeViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ExpenseTypeViewModel expenseTypeViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _expenseTypeAppService.Create(expenseTypeViewModel);
            return Ok(new { id });
        }

        
    }
}
