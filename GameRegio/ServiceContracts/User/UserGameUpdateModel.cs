using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.User
{
    public class UserGameUpdateModel
    {
        public string UserGameId { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string GameUserName { get; set; }
    }
}
