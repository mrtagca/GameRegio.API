using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Transactions : MongoDbEntity,IDisposable
    {
        public string WalletId { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public string TransactionName { get; set; }
        public DateTime TransactionTime { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
