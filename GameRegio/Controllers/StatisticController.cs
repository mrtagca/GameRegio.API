using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Statistic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticDataAccess _statisticDataAccess;

        public StatisticController(IStatisticDataAccess statisticDataAccess)
        {
            this._statisticDataAccess = statisticDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _statisticDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(StatisticGetByIdModel statisticGetByIdModel)
        {
            var result = _statisticDataAccess.GetByIdAsync(statisticGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Statistics statistics)
        {
            var statistic = _statisticDataAccess.AddAsync(new Statistics()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = statistics.UserId,
                GameId = statistics.GameId,
                WinCount = statistics.WinCount,
                LostCount = statistics.LostCount

            });

            if (statistic == null)
                return BadRequest(new { message = "İstatistik eklenemedi!" });

            return Ok(new { statistic.Result });
        }

        [HttpPost]
        public IActionResult Update(StatisticUpdateModel statisticUpdateModel)
        {
            try
            {

                var statistic = _statisticDataAccess.UpdateAsync(statisticUpdateModel.StatisticId.ToString(), new Statistics()
                {
                    UserId = statisticUpdateModel.UserId,
                    GameId = statisticUpdateModel.GameId,
                    WinCount = statisticUpdateModel.WinCount,
                    LostCount = statisticUpdateModel.LostCount
                }).Result;

                statistic = _statisticDataAccess.GetByIdAsync(statisticUpdateModel.StatisticId.ToString()).Result;

                if (statistic == null)
                    return BadRequest(new { message = "İstatistik güncellenemedi!" });

                return Ok(new { statistic });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(StatisticDeleteModel statisticDeleteModel)
        {
            var result = _statisticDataAccess.DeleteAsync(statisticDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("İstatistik silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
