using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Duels : MongoDbEntity
    {
        public string HomeUserId { get; set; }
        public string AwayUserId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string GameId { get; set; }
        public decimal DuelCoin { get; set; }
        public decimal Award { get; set; }
        public int Status { get; set; }
        public string WinnerUserId { get; set; }
        public DateTime EndTime { get; set; }
    }
}
