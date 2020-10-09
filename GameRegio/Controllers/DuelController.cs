using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Duel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DuelController : ControllerBase
    {
        private readonly IDuelDataAccess _duelDataAccess;

        public DuelController(IDuelDataAccess duelDataAccess)
        {
            this._duelDataAccess = duelDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _duelDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(DuelGetByIdModel duelGetByIdModel)
        {
            var result = _duelDataAccess.GetByIdAsync(duelGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Duels duels)
        {
            var game = _duelDataAccess.AddAsync(new Duels()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                HomeUserId = duels.HomeUserId,
                AwayUserId = duels.AwayUserId,
                HomeScore = duels.HomeScore,
                AwayScore = duels.AwayScore,
                GameId = duels.GameId,
                DuelCoin = duels.DuelCoin,
                Award = duels.Award,
                Status = duels.Status,
                WinnerUserId = duels.WinnerUserId,
                EndTime = duels.EndTime

            });

            if (game == null)
                return BadRequest(new { message = "Duello eklenemedi!" });

            return Ok(new { game.Result });
        }

        [HttpPost]
        public IActionResult Update(DuelUpdateModel duelUpdateModel)
        {
            try
            {

                var game = _duelDataAccess.UpdateAsync(duelUpdateModel.DuelId.ToString(), new Duels()
                {
                    HomeUserId = duelUpdateModel.HomeUserId,
                    AwayUserId = duelUpdateModel.AwayUserId,
                    HomeScore = duelUpdateModel.HomeScore,
                    AwayScore = duelUpdateModel.AwayScore,
                    GameId = duelUpdateModel.GameId,
                    DuelCoin = duelUpdateModel.DuelCoin,
                    Award = duelUpdateModel.Award,
                    Status = duelUpdateModel.Status,
                    WinnerUserId = duelUpdateModel.WinnerUserId,
                    EndTime = duelUpdateModel.EndTime

                }).Result;

                game = _duelDataAccess.GetByIdAsync(duelUpdateModel.DuelId.ToString()).Result;

                if (game == null)
                    return BadRequest(new { message = "Duello güncellenemedi!" });

                return Ok(new { game });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(DuelDeleteModel duelDeleteModel)
        {
            var result = _duelDataAccess.DeleteAsync(duelDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Duello silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
