using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigDataAccess _configDataAccess;

        public ConfigController(IConfigDataAccess configDataAccess)
        {
            this._configDataAccess = configDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _configDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(ConfigGetByIdModel configGetByIdModel)
        {
            var result = _configDataAccess.GetByIdAsync(configGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Config configs)
        {
            var config = _configDataAccess.AddAsync(new Config()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Brand = configs.Brand,
                Logo = configs.Logo,
                Description = configs.Description,
                Facebook = configs.Facebook,
                Twitter = configs.Twitter,
                Instagram = configs.Instagram,
                Youtube =configs.Youtube,
                Discord = configs.Discord,
                Linkedin = configs.Linkedin,
                Twitch = configs.Twitch,
                Email = configs.Email,
                Address = configs.Address,
                Phone = configs.Phone,
                Banner = configs.Banner,
                ApiUrl = configs.ApiUrl,
                ServerIp = configs.ServerIp,
                OneSignalJson = configs.OneSignalJson,
                RelatedJson = configs.RelatedJson

            });

            if (config == null)
                return BadRequest(new { message = "Config eklenemedi!" });

            return Ok(new { config.Result });
        }

        [HttpPost]
        public IActionResult Update(ConfigUpdateModel configUpdateModel)
        {
            try
            {

                var device = _configDataAccess.UpdateAsync(configUpdateModel.ConfigId.ToString(), new Config()
                {
                    Brand = configUpdateModel.Brand,
                    Logo = configUpdateModel.Logo,
                    Description = configUpdateModel.Description,
                    Facebook = configUpdateModel.Facebook,
                    Twitter = configUpdateModel.Twitter,
                    Instagram = configUpdateModel.Instagram,
                    Youtube = configUpdateModel.Youtube,
                    Discord = configUpdateModel.Discord,
                    Linkedin = configUpdateModel.Linkedin,
                    Twitch = configUpdateModel.Twitch,
                    Email = configUpdateModel.Email,
                    Address = configUpdateModel.Address,
                    Phone = configUpdateModel.Phone,
                    Banner = configUpdateModel.Banner,
                    ApiUrl = configUpdateModel.ApiUrl,
                    ServerIp = configUpdateModel.ServerIp,
                    OneSignalJson = configUpdateModel.OneSignalJson,
                    RelatedJson = configUpdateModel.RelatedJson

                }).Result;

                device = _configDataAccess.GetByIdAsync(configUpdateModel.ConfigId.ToString()).Result;

                if (device == null)
                    return BadRequest(new { message = "Config güncellenemedi!" });

                return Ok(new { device });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(ConfigDeleteModel configDeleteModel)
        {
            var result = _configDataAccess.DeleteAsync(configDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Config silinemedi!");
            }

            return Ok(new { result });
        }
    }
}
