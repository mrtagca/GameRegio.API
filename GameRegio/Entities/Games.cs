using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Games : MongoDbEntity,IDisposable
    {
        public string GameName { get; set; }
        public string GameLogo { get; set; }
        public string DuelBanner { get; set; }
        public string TournamentBanner { get; set; }
        public bool Pc { get; set; }
        public bool Mobile { get; set; }
        public bool Playstation { get; set; }
        public bool Xbox { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
