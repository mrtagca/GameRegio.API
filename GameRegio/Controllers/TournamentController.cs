using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Tournament;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentDataAccess _tournamentDataAccess;

        public TournamentController(ITournamentDataAccess tournamentDataAccess)
        {
            this._tournamentDataAccess = tournamentDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _tournamentDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(TournamentGetByIdModel tournamentGetByIdModel)
        {
            var result = _tournamentDataAccess.GetByIdAsync(tournamentGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Tournaments tournaments)
        {
            var tournament = _tournamentDataAccess.AddAsync(new Tournaments()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                GameId = tournaments.GameId,
                UserCount = tournaments.UserCount,
                Reward = tournaments.Reward,
                GameMinute = tournaments.GameMinute,
                StartTime = tournaments.StartTime,
                EndTime = tournaments.EndTime

            });

            if (tournament == null)
                return BadRequest(new { message = "Turnuva eklenemedi!" });

            return Ok(new { tournament.Result });
        }

        [HttpPost]
        public IActionResult Update(TournamentUpdateModel tournamentUpdateModel)
        {
            try
            {

                var transaction = _tournamentDataAccess.UpdateAsync(tournamentUpdateModel.TournamentId.ToString(), new Tournaments()
                {
                    GameId = tournamentUpdateModel.GameId,
                    UserCount = tournamentUpdateModel.UserCount,
                    Reward = tournamentUpdateModel.Reward,
                    GameMinute = tournamentUpdateModel.GameMinute,
                    StartTime = tournamentUpdateModel.StartTime,
                    EndTime = tournamentUpdateModel.EndTime
                }).Result;

                transaction = _tournamentDataAccess.GetByIdAsync(tournamentUpdateModel.TournamentId.ToString()).Result;

                if (transaction == null)
                    return BadRequest(new { message = "Turnuva güncellenemedi!" });

                return Ok(new { transaction });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(TournamentDeleteModel tournamentDeleteModel)
        {
            var result = _tournamentDataAccess.DeleteAsync(tournamentDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Turnuva silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
