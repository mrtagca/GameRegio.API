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
            var wallet = _walletDataAccess.AddAsync(wallets);

            if (wallet == null)
                return BadRequest(new { message = "Wallet eklenemedi!" });

            return Ok(new { wallet.Result });
        }

        [HttpPost]
        public IActionResult Update(WalletUpdateModel walletUpdateModel)
        {
            try
            {
                Wallets findWallet = _walletDataAccess.GetByIdAsync(walletUpdateModel.WalletId.ToString()).Result;
                findWallet.UserId = walletUpdateModel.UserId;
                findWallet.CurrentBalance = walletUpdateModel.CurrentBalance;
                findWallet.PaparaId = walletUpdateModel.PaparaId;
                findWallet.UpdatedAt = DateTime.UtcNow.AddHours(3);


                var wallet = _walletDataAccess.UpdateAsync(walletUpdateModel.WalletId.ToString(), findWallet).Result;
                wallet = _walletDataAccess.GetByIdAsync(walletUpdateModel.WalletId.ToString()).Result;

                if (wallet == null)
                    return BadRequest(new { message = "User eklenemedi!" });

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
                return BadRequest("Not found");
            }

            return Ok(new { result });
        }
    }
}