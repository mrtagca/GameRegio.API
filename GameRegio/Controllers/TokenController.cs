using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Abstract;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
   
    public class TokenController : Controller
    {
        private readonly IUserDataAccess _userDataAccess;
        IUserService _userService;
        
        public TokenController(IUserDataAccess userDataAccess, IUserService userService)
        {
            this._userDataAccess = userDataAccess;
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorret" });

            return Ok(new { Username = user.Value.username, Token = user.Value.token });
        }
    }
}