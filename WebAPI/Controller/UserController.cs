using Application.Interfaces;
using Application.Services;
using Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helper;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService) 
        { 
            _userAppService = userAppService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {

            //var all = await _userAppService.Register();
            var registeredUser = await _userAppService.GetById(id);
            

            return Ok(registeredUser);
        }
    }
}
