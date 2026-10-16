using Application.DTO.Input.Account;
using Application.DTO.Output.Account;
using Application.Handlers.Account;
using Infrastructure.CrossCutting.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[ApiController]
[Route("api/account")]
public class AccountController : ApiController
{
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(AuthTokensOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> PostAsync(
        [FromBody] CreateAccountInput newAccount,
        [FromServices] CreateAccountHandler createAccountHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthTokensOutput tokens = await createAccountHandler.ExecuteAsync(newAccount);

        return Created(string.Empty, tokens);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthTokensOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> LogIn(
        [FromBody] LoginInput loginInput,
        [FromServices] LoginAccountHandler loginAccountHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthTokensOutput tokens = await loginAccountHandler.ExecuteAsync(loginInput);

        return Ok(tokens);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthTokensOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Refresh(
        [FromBody] RefreshTokenInput refreshTokenInput,
        [FromServices] RefreshTokenHandler refreshTokenHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        AuthTokensOutput tokens = await refreshTokenHandler.ExecuteAsync(refreshTokenInput);

        return Ok(tokens);
    }

    [AllowAnonymous]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Logout(
        [FromBody] RefreshTokenInput refreshTokenInput,
        [FromServices] LogoutHandler logoutHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await logoutHandler.ExecuteAsync(refreshTokenInput);

        return NoContent();
    }
}
