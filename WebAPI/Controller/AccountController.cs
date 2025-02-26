using Application.Interfaces;
using Application.ViewModel.Account;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;


[ApiController]
[Route("api/account")]
public class AccountController(IAccountAppService accountAppService) : ApiController
{
    private readonly IAccountAppService _accountAppService = accountAppService;

    [HttpPost("sign-up")]
    public async Task<ActionResult> PostAsync([FromBody] CreateNewAccountViewModel newAccount)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid id = await _accountAppService.Create(newAccount);

        return Created(string.Empty, new { id });
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> LogIn([FromBody] LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string token = await _accountAppService.LogIn(loginViewModel);

        return Ok(new { token });
    }
}
