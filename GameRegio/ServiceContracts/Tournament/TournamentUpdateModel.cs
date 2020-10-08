using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Tournament
{
    public class TournamentUpdateModel : IDisposable
    {
        public string TournamentId { get; set; }
        public string GameId { get; set; }
        public int UserCount { get; set; }
        public decimal Reward { get; set; }
        public int GameMinute { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
