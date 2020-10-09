using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Game
{
    public class GameUpdateModel
    {
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string GameLogo { get; set; }
        public string DuelBanner { get; set; }
        public string TournamentBanner { get; set; }
        public bool Pc { get; set; }
        public bool Mobile { get; set; }
        public bool Playstation { get; set; }
        public bool Xbox { get; set; }
    }
}
