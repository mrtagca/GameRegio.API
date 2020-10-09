using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.DeviceToken
{
    public class DeviceTokenUpdateModel
    {
        public string DeviceTokenId { get; set; }
        public string UserId { get; set; }
        public string Platform { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}
