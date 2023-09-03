using Application.Interfaces;
using Application.Services;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
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


        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {

            var registeredUser = await _userAppService.GetById(id);
            

            return Ok(registeredUser);
        }

        public async Task<ActionResult> GetAll ()
        {
            var id  = _userContext.GetUserId();
            var users = await _userAppService.GetAll();

            return Ok(users);
        }
    }
}
