using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Permit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly IPermitDataAccess _permitDataAccess;

        public PermitController(IPermitDataAccess permitDataAccess)
        {
            this._permitDataAccess = permitDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _permitDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(PermitGetByIdModel permitGetByIdModel)
        {
            var result = _permitDataAccess.GetByIdAsync(permitGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Permits permits)
        {
            var permit = _permitDataAccess.AddAsync(new Permits()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = permits.UserId,
                EmailPermit = permits.EmailPermit,
                AppPermit = permits.AppPermit,
                WebPermit = permits.WebPermit

            });

            if (permit == null)
                return BadRequest(new { message = "Permit eklenemedi!" });

            return Ok(new { permit.Result });
        }

        [HttpPost]
        public IActionResult Update(PermitUpdateModel permitUpdateModel)
        {
            try
            {

                var permit = _permitDataAccess.UpdateAsync(permitUpdateModel.PermitId.ToString(), new Permits()
                {
                    UserId = permitUpdateModel.UserId,
                    EmailPermit=permitUpdateModel.EmailPermit,
                    AppPermit = permitUpdateModel.AppPermit,
                    WebPermit = permitUpdateModel.WebPermit

                }).Result;

                permit = _permitDataAccess.GetByIdAsync(permitUpdateModel.PermitId.ToString()).Result;

                if (permit == null)
                    return BadRequest(new { message = "Permit güncellenemedi!" });

                return Ok(new { permit });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(PermitDeleteModel permitDeleteModel)
        {
            var result = _permitDataAccess.DeleteAsync(permitDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Permit silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
