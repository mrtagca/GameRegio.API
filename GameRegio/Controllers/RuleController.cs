using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Rule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleDataAccess _ruleDataAccess;

        public RuleController(IRuleDataAccess ruleDataAccess)
        {
            this._ruleDataAccess = ruleDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _ruleDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(RuleGetByIdModel ruleGetByIdModel)
        {
            var result = _ruleDataAccess.GetByIdAsync(ruleGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Rules rules)
        {
            var rule = _ruleDataAccess.AddAsync(new Rules()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                RuleName = rules.RuleName,
                RuleDescription = rules.RuleDescription,
                GameId = rules.GameId,
                ModId = rules.ModId,
                IsActive = rules.IsActive

            });

            if (rule == null)
                return BadRequest(new { message = "Rule eklenemedi!" });

            return Ok(new { rule.Result });
        }

        [HttpPost]
        public IActionResult Update(RuleUpdateModel ruleUpdateModel)
        {
            try
            {

                var rule = _ruleDataAccess.UpdateAsync(ruleUpdateModel.RuleId.ToString(), new Rules()
                {
                    RuleName = ruleUpdateModel.RuleName,
                    RuleDescription = ruleUpdateModel.RuleDescription,
                    GameId = ruleUpdateModel.GameId,
                    ModId = ruleUpdateModel.ModId,
                    IsActive = ruleUpdateModel.IsActive

                }).Result;

                rule = _ruleDataAccess.GetByIdAsync(ruleUpdateModel.RuleId.ToString()).Result;

                if (rule == null)
                    return BadRequest(new { message = "Rule güncellenemedi!" });

                return Ok(new { rule });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(RuleDeleteModel ruleDeleteModel)
        {
            var result = _ruleDataAccess.DeleteAsync(ruleDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Rule silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
