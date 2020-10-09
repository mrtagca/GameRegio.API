using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Mod
{
    public class ModUpdateModel
    {
        public string ModId { get; set; }
        public string ModName { get; set; }
        public string GameId { get; set; }
    }
}
