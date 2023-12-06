using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{        
    [Authorize]
    [ApiController]
    [Route("api/expenses-installments")]
    public class ExpenseInstallmentController : ApiController
    {
        private readonly IExpenseInstallmentAppService _expenseInstallmentAppService;

        public ExpenseInstallmentController(IExpenseInstallmentAppService expenseInstallmentAppService)
        {
            _expenseInstallmentAppService = expenseInstallmentAppService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var expenseInstallment = await _expenseInstallmentAppService.Get(id);

            return OkFind(expenseInstallment);
        }

        [HttpGet("date/{year:int}/{month:int}/{day:int}")]
        public IActionResult GetByDate(int year, int month, int day)
        {
            var expenseInstallments = _expenseInstallmentAppService.GetByDate(year, month, day);  
            return Ok(expenseInstallments);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id)
        {
            
            return OkFind(new { });
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {

            return OkFind(new { });
        }
    }
}
