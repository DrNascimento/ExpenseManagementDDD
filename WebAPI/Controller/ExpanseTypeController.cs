using Application.Interfaces;
using Application.ViewModel.ExpanseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/expanse-type")]
    public class ExpanseTypeController : ApiController
    {
        private readonly IExpanseTypeAppService _expanseTypeAppService;

        public ExpanseTypeController(IExpanseTypeAppService expanseTypeAppService)
        {
            _expanseTypeAppService = expanseTypeAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expanses = _expanseTypeAppService.GetAll();

            return Ok(expanses);
        }

        [HttpGet("{int:id}")]
        public async Task<IActionResult> Get(int id)
        {
            var expanseViewModel = await _expanseTypeAppService.GetById(id);
            return expanseViewModel is null ? NotFound() : Ok(expanseViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ExpanseTypeViewModel expanseTypeViewModel)
        {
            var id = await _expanseTypeAppService.Create(expanseTypeViewModel);
            return Ok(new { id });
        }

        
    }
}
