﻿using Application.Interfaces;
using Infrastructure.CrossCutting.Identity;
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

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var registeredUser = await _userAppService.GetById(id);

            return Ok(registeredUser);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult GetAll ()
        {
            var users = _userAppService.GetAll();

            return Ok(users);
        }

        [HttpGet("profile")]
        public async Task<ActionResult> GetProfile()
        {
            var currentUser = Convert.ToInt16(_userContext.UserId);
            return Ok(await _userAppService.GetById(currentUser));
        }
    }
}
