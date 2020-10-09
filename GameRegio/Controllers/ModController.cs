using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Mod;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ModController : ControllerBase
    {
        private readonly IModDataAccess _modDataAccess;

        public ModController(IModDataAccess modDataAccess)
        {
            this._modDataAccess = modDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _modDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(ModGetByIdModel modGetByIdModel)
        {
            var result = _modDataAccess.GetByIdAsync(modGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Mods mods)
        {
            var mod = _modDataAccess.AddAsync(new Mods()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ModName = mods.ModName,
                GameId = mods.GameId

            });

            if (mod == null)
                return BadRequest(new { message = "Mod eklenemedi!" });

            return Ok(new { mod.Result });
        }

        [HttpPost]
        public IActionResult Update(ModUpdateModel modUpdateModel)
        {
            try
            {

                var mod = _modDataAccess.UpdateAsync(modUpdateModel.ModId.ToString(), new Mods()
                {
                  ModName = modUpdateModel.ModName,
                  GameId = modUpdateModel.GameId

                }).Result;

                mod = _modDataAccess.GetByIdAsync(modUpdateModel.ModId.ToString()).Result;

                if (mod == null)
                    return BadRequest(new { message = "Permit güncellenemedi!" });

                return Ok(new { mod });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(ModDeleteModel modDeleteModel)
        {
            var result = _modDataAccess.DeleteAsync(modDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Permit silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
