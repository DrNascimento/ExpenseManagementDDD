using Application.DTO.Input.User;
using Application.DTO.Output.User;
using Application.Handlers.User;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IUserContext userContext) : ApiController
{
    [Authorize(Roles = "admin")]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetById(
        Guid id,
        [FromServices] GetUserByIdHandler getUserByIdHandler)
    {
        var registeredUser = await getUserByIdHandler.ExecuteAsync(id);

        return Ok(registeredUser);
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserOutput>), 200)]
    public ActionResult GetAll([FromServices] GetAllUsersHandler getAllUsersHandler)
    {
        IEnumerable<UserOutput> users = getAllUsersHandler.Execute();

        return Ok(users);
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserOutput), 200)]
    public async Task<ActionResult> GetProfile([FromServices] GetUserByIdHandler getUserByIdHandler)
    {
        UserOutput user = await getUserByIdHandler.ExecuteAsync(userContext.UserId);
        return Ok(user);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(
        Guid id,
        [FromBody] UpdateUserInput updateUserInput,
        [FromServices] UpdateUserHandler updateUserHandler)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await updateUserHandler.ExecuteAsync(id, updateUserInput);

        return Ok();
    }
}
