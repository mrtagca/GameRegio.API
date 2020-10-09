using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.DeviceToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DeviceTokenController : ControllerBase
    {
        private readonly IDeviceTokenDataAccess _deviceTokenDataAccess;

        public DeviceTokenController(IDeviceTokenDataAccess deviceTokenDataAccess)
        {
            this._deviceTokenDataAccess = deviceTokenDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _deviceTokenDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(DeviceTokenGetByIdModel deviceTokenGetByIdModel)
        {
            var result = _deviceTokenDataAccess.GetByIdAsync(deviceTokenGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(DeviceTokens deviceTokens)
        {
            var deviceToken = _deviceTokenDataAccess.AddAsync(new DeviceTokens()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = deviceTokens.UserId,
                Platform = deviceTokens.Platform,
                Token = deviceTokens.Token,
                IsActive = deviceTokens.IsActive

            });

            if (deviceToken == null)
                return BadRequest(new { message = "Device Token eklenemedi!" });

            return Ok(new { deviceToken.Result });
        }

        [HttpPost]
        public IActionResult Update(DeviceTokenUpdateModel deviceTokenUpdateModel)
        {
            try
            {

                var deviceToken = _deviceTokenDataAccess.UpdateAsync(deviceTokenUpdateModel.DeviceTokenId.ToString(), new DeviceTokens()
                {
                    UserId = deviceTokenUpdateModel.UserId,
                    Platform = deviceTokenUpdateModel.Platform,
                    Token = deviceTokenUpdateModel.Token,
                    IsActive = deviceTokenUpdateModel.IsActive
                }).Result;

                deviceToken = _deviceTokenDataAccess.GetByIdAsync(deviceTokenUpdateModel.DeviceTokenId.ToString()).Result;

                if (deviceToken == null)
                    return BadRequest(new { message = "Device Token güncellenemedi!" });

                return Ok(new { deviceToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(DeviceTokenDeleteModel deviceTokenDeleteModel)
        {
            var result = _deviceTokenDataAccess.DeleteAsync(deviceTokenDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Device Token silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
