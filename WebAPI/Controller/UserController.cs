using Application.Interfaces;
using Application.ViewModel.User;
using Infrastructure.CrossCutting.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;
        private readonly IUserContext _userContext;

        public UserController(IUserAppService userAppService,
                              IUserContext userContext) 
        { 
            _userAppService = userAppService;
            _userContext = userContext;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var registeredUser = await _userAppService.GetById(id);

            return Ok(registeredUser);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult GetAll()
        {
            var users = _userAppService.GetAll();

            return Ok(users);
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            return Ok(await _userAppService.GetById(_userContext.UserId));
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
}
