using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Wallet
{
    public class WalletUpdateModel : IDisposable
    {
        public string WalletId { get; set; }
        public string UserId { get; set; }
        public double CurrentBalance { get; set; }
        public string PaparaId { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
