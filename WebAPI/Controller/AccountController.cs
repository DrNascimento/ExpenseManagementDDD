using Application.Interfaces;
using Application.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{

    [ApiController]
    [Route("api/account")]
    public class AccountController : ApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly IAccountAppService _accountAppService;

        public AccountController(IUserAppService userAppService, IAccountAppService accountAppService)
        {
            _userAppService = userAppService;
            _accountAppService = accountAppService;
        }


        [HttpPost("sign-up")]
        public async Task<ActionResult> PostAsync([FromBody] NewAccountViewModel newAccount)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _accountAppService.Create(newAccount);


            return Created(string.Empty, new { id });
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _accountAppService.LogIn(loginViewModel);


            return Ok();
        }
    }
}
