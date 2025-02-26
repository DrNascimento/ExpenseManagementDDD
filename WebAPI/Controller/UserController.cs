using Application.Interfaces;
using Application.ViewModel;
using Application.ViewModel.User;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IUserAppService userAppService, IUserContext userContext) : ApiController
{
    private readonly IUserAppService _userAppService = userAppService;
    private readonly IUserContext _userContext = userContext;

    [Authorize(Roles = "admin")]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var registeredUser = await _userAppService.GetById(id);

        return Ok(registeredUser);
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserViewModel>), 200)]
    public ActionResult GetAll()
    {
        IEnumerable<UserViewModel> users = _userAppService.GetAll();

        return Ok(users);
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public async Task<ActionResult> GetProfile()
    {
        UserViewModel user = await _userAppService.GetById(_userContext.UserId);
        return Ok(user);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserViewModel updateUserViewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _userAppService.Update(id, updateUserViewModel);

        return Ok();
    }
}
