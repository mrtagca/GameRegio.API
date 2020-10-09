using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceDataAccess _deviceDataAccess;

        public DeviceController(IDeviceDataAccess deviceDataAccess)
        {
            this._deviceDataAccess = deviceDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _deviceDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(DeviceGetByIdModel deviceGetByIdModel)
        {
            var result = _deviceDataAccess.GetByIdAsync(deviceGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Devices devices)
        {
            var device = _deviceDataAccess.AddAsync(new Devices()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = devices.UserId,
                DeviceName = devices.DeviceName,
                Brand = devices.Brand,
                Model = devices.Model,
                Carrier = devices.Carrier,
                OperatingSystem = devices.OperatingSystem,
                OperatingSystemVersion = devices.OperatingSystemVersion,
                Language = devices.Language,
                IpAddress = devices.IpAddress,
                MacAddress = devices.MacAddress

            });

            if (device == null)
                return BadRequest(new { message = "Device eklenemedi!" });

            return Ok(new { device.Result });
        }

        [HttpPost]
        public IActionResult Update(DeviceUpdateModel deviceUpdateModel)
        {
            try
            {

                var device = _deviceDataAccess.UpdateAsync(deviceUpdateModel.DeviceId.ToString(), new Devices()
                {
                    UserId = deviceUpdateModel.UserId,
                    DeviceName = deviceUpdateModel.DeviceName,
                    Brand = deviceUpdateModel.Brand,
                    Model = deviceUpdateModel.Model,
                    Carrier = deviceUpdateModel.Carrier,
                    OperatingSystem = deviceUpdateModel.OperatingSystem,
                    OperatingSystemVersion = deviceUpdateModel.OperatingSystemVersion,
                    Language = deviceUpdateModel.Language,
                    IpAddress = deviceUpdateModel.IpAddress,
                    MacAddress = deviceUpdateModel.MacAddress

                }).Result;

                device = _deviceDataAccess.GetByIdAsync(deviceUpdateModel.DeviceId.ToString()).Result;

                if (device == null)
                    return BadRequest(new { message = "Device güncellenemedi!" });

                return Ok(new { device });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(DeviceDeleteModel deviceDeleteModel)
        {
            var result = _deviceDataAccess.DeleteAsync(deviceDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Device silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
