using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class DeviceTokens : MongoDbEntity,IDisposable
    {
        public string UserId { get; set; }
        public string Platform { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
