using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Abstract;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameRegio.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserController(IUserDataAccess userDataAccess)
        {
            this._userDataAccess = userDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _userDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }


        [HttpGet]
        public IActionResult GetById(UserGetByIdModel userGetByIdModel)
        {
            var result = _userDataAccess.GetByIdAsync(userGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Users users)
        {
            var user = _userDataAccess.AddAsync(users);

            if (user == null)
                return BadRequest(new { message = "User eklenemedi!" });

            return Ok(new { user });
        }

        [HttpPost]
        public IActionResult Update(UserUpdateModel userUpdateModel)
        {
            try
            {
                Users findUser = _userDataAccess.GetByIdAsync(userUpdateModel.ObjectId.ToString()).Result;
                findUser.Email = userUpdateModel.Email;
                findUser.Password = userUpdateModel.Password;
                findUser.CreatedAt = DateTime.Now;


                var user = _userDataAccess.UpdateAsync(userUpdateModel.ObjectId.ToString(),findUser).Result;

                if (user == null)
                    return BadRequest(new { message = "User eklenemedi!" });

                return Ok(new { user });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteModel userDeleteModel)
        {
            var result = _userDataAccess.DeleteAsync(userDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(new { result });
        }
    }
}