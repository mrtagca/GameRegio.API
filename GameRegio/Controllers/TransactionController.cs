using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRegio.Entities;
using GameRegio.Interface;
using GameRegio.ServiceContracts.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GameRegio.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDataAccess _transactionDataAccess;

        public TransactionController(ITransactionDataAccess transactionDataAccess)
        {
            this._transactionDataAccess = transactionDataAccess;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _transactionDataAccess.Get();
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result.ToList());
        }

        [HttpGet]
        public IActionResult GetById(TransactionGetByIdModel transactionGetByIdModel)
        {
            var result = _transactionDataAccess.GetByIdAsync(transactionGetByIdModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Not found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Transactions transactions)
        {
            var wallet = _transactionDataAccess.AddAsync(new Transactions()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                WalletId = transactions.WalletId,
                UserId = transactions.UserId,
                Balance = transactions.Balance,
                TransactionName = transactions.TransactionName,
                TransactionTime = DateTime.Now.AddHours(3)

            });

            if (wallet == null)
                return BadRequest(new { message = "Wallet eklenemedi!" });

            return Ok(new { wallet.Result });
        }

        [HttpPost]
        public IActionResult Update(TransactionUpdateModel transactionUpdateModel)
        {
            try
            {

                var transaction = _transactionDataAccess.UpdateAsync(transactionUpdateModel.TransactionId.ToString(), new Transactions()
                {
                    WalletId = transactionUpdateModel.WalletId,
                    UserId = transactionUpdateModel.UserId,
                    Balance = transactionUpdateModel.Balance,
                    TransactionName = transactionUpdateModel.TransactionName,
                    TransactionTime = transactionUpdateModel.TransactionTime
                }).Result;

                transaction = _transactionDataAccess.GetByIdAsync(transactionUpdateModel.TransactionId.ToString()).Result;

                if (transaction == null)
                    return BadRequest(new { message = "Transaction güncellenemedi!" });

                return Ok(new { transaction });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Delete(TransactionDeleteModel transactionDeleteModel)
        {
            var result = _transactionDataAccess.DeleteAsync(transactionDeleteModel.Id).Result;
            if (result == null)
            {
                return BadRequest("Wallet silinemedi!");
            }

            return Ok(new { result });
        }

    }
}
