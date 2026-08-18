using Application.DTO.Input.Account;
using Application.Handlers.Account;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[ApiController]
[Route("api/account")]
public class AccountController : ApiController
{
    [HttpPost("sign-up")]
    public async Task<ActionResult> PostAsync(
        [FromBody] CreateAccountInput newAccount,
        [FromServices] CreateAccountHandler createAccountHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Guid id = await createAccountHandler.ExecuteAsync(newAccount);

        return Created(string.Empty, new { id });
    }

    [HttpPost("login")]
    public async Task<ActionResult> LogIn(
        [FromBody] LoginInput loginInput,
        [FromServices] LoginAccountHandler loginAccountHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string token = await loginAccountHandler.ExecuteAsync(loginInput);

        return Ok(new { token });
    }
}
