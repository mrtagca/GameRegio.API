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
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDataAccess _userDataAccess;
        private readonly IUserGameDataAccess _userGameDataAccess;

        public UserController(IUserDataAccess userDataAccess, IUserGameDataAccess userGameDataAccess)
        {
            this._userDataAccess = userDataAccess;
            this._userGameDataAccess = userGameDataAccess;
        }

        #region Users
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
            var user = _userDataAccess.AddAsync(new Users()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Username = users.Username,
                Email = users.Email,
                Password = users.Password,
                Name = users.Name,
                Age = users.Age,
                Phone = users.Phone,
                ProfilePhoto = users.ProfilePhoto,
                Bio = users.Bio,
                IsBanned = 0,
                IsDeleted = 0
            });

            if (user == null)
                return BadRequest(new { message = "User eklenemedi!" });

            return Ok(new { user });
        }

        [HttpPost]
        public IActionResult Update(UserUpdateModel userUpdateModel)
        {
            try
            {
                Users findUser = _userDataAccess.GetByIdAsync(userUpdateModel.UserId.ToString()).Result;
                findUser.Email = userUpdateModel.Email;
                findUser.Password = userUpdateModel.Password;
                findUser.CreatedAt = DateTime.Now;


                var user = _userDataAccess.UpdateAsync(userUpdateModel.UserId.ToString(), findUser).Result;

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
        #endregion

        #region UserGames
        [HttpGet]
        public IActionResult GetAllUserGames()
        {
            var result = _userGameDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetUserGameById(UserGameGetByIdModel userGameGetByIdModel)
        {
            var result = _userGameDataAccess.GetByIdAsync(userGameGetByIdModel.UserGameId).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUserGame(UserGames userGames)
        {
            var userGame = _userGameDataAccess.AddAsync(new UserGames()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                GameId = userGames.GameId,
                UserId = userGames.UserId,
                GameUserName = userGames.GameUserName
            });

            if (userGame == null)
                return BadRequest(new { message = "User eklenemedi!" });

            return Ok(new { userGame.Result });
        }

        [HttpPost]
        public IActionResult UpdateUserGame(UserGameUpdateModel userGameUpdateModel)
        {
            try
            {
                UserGames findUserGames = _userGameDataAccess.GetByIdAsync(userGameUpdateModel.UserGameId.ToString()).Result;
                //findUserGames.Id = userGameUpdateModel.UserGameId.ToString();
                findUserGames.GameId = userGameUpdateModel.GameId;
                findUserGames.UserId = userGameUpdateModel.UserId;
                findUserGames.GameUserName = userGameUpdateModel.GameUserName;



                var userGame = _userGameDataAccess.UpdateAsync(userGameUpdateModel.UserGameId.ToString(), findUserGames).Result;

                if (userGame == null)
                    return BadRequest(new { message = "User eklenemedi!" });

                return Ok(new { userGame });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult DeleteUserGame(UserGameDeleteModel userGameDeleteModel)
        {
            var result = _userDataAccess.DeleteAsync(userGameDeleteModel.UserGameId).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(new { result });
        }
        #endregion
    }
}