using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletDataAccess _walletDataAccess;

        public WalletController(IWalletDataAccess walletDataAccess)
        {
            this._walletDataAccess = walletDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _walletDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(WalletGetByIdModel walletGetByIdModel)
        {
            var result = _walletDataAccess.GetByIdAsync(walletGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Wallets wallets)
        {
            var wallet = _walletDataAccess.AddAsync(new Wallets()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserId = wallets.UserId,
                CurrentBalance = wallets.CurrentBalance,
                PaparaId = wallets.PaparaId

            });

            if (wallet == null)
                return BadRequest(new { message = "Wallet eklenemedi!" });

            return Ok(new { wallet.Result });
        }

        [HttpPost]
        public IActionResult Update(WalletUpdateModel walletUpdateModel)
        {
            try
            {

                var wallet = _walletDataAccess.UpdateAsync(walletUpdateModel.WalletId.ToString(), new Wallets() {
                    UserId = walletUpdateModel.UserId,
                    CurrentBalance = walletUpdateModel.CurrentBalance,
                    PaparaId = walletUpdateModel.PaparaId,
                    UpdatedAt = DateTime.UtcNow.AddHours(3)
                }).Result;

                wallet = _walletDataAccess.GetByIdAsync(walletUpdateModel.WalletId.ToString()).Result;

                if (wallet == null)
                    return BadRequest(new { message = "Wallet güncellenemedi!" });

                return Ok(new { wallet });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(WalletDeleteModel walletDeleteModel)
        {
            var result = _walletDataAccess.DeleteAsync(walletDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Wallet silinemedi!");
            }

            return Ok(new { result });
        }
    }
}