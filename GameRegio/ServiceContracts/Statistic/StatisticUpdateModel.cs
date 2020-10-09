using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Statistic
{
    public class StatisticUpdateModel
    {
        public string StatisticId { get; set; }
        public string UserId { get; set; }
        public string GameId { get; set; }
        public int WinCount { get; set; }
        public int LostCount { get; set; }
    }
}
