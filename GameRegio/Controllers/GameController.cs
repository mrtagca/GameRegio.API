using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Game;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameDataAccess _gameDataAccess;

        public GameController(IGameDataAccess gameDataAccess)
        {
            this._gameDataAccess = gameDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _gameDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(GameByIdModel gameByIdModel)
        {
            var result = _gameDataAccess.GetByIdAsync(gameByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Games games)
        {
            var game = _gameDataAccess.AddAsync(new Games()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                GameName = games.GameName,
                GameLogo = games.GameLogo,
                DuelBanner = games.DuelBanner,
                TournamentBanner = games.TournamentBanner,
                Pc = games.Pc,
                Mobile = games.Mobile,
                Playstation=games.Playstation,
                Xbox=games.Xbox

            });

            if (game == null)
                return BadRequest(new { message = "Game eklenemedi!" });

            return Ok(new { game.Result });
        }

        [HttpPost]
        public IActionResult Update(GameUpdateModel gameUpdateModel)
        {
            try
            {

                var game = _gameDataAccess.UpdateAsync(gameUpdateModel.GameId.ToString(), new Games()
                {
                   GameName = gameUpdateModel.GameName,
                   GameLogo = gameUpdateModel.GameLogo,
                   DuelBanner = gameUpdateModel.DuelBanner,
                   TournamentBanner = gameUpdateModel.TournamentBanner,
                   Pc = gameUpdateModel.Pc,
                   Mobile = gameUpdateModel.Mobile,
                   Playstation = gameUpdateModel.Playstation,
                   Xbox = gameUpdateModel.Xbox

                }).Result;

                game = _gameDataAccess.GetByIdAsync(gameUpdateModel.GameId.ToString()).Result;

                if (game == null)
                    return BadRequest(new { message = "Game güncellenemedi!" });

                return Ok(new { game });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(GameDeleteModel gameDeleteModel)
        {
            var result = _gameDataAccess.DeleteAsync(gameDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Game silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
