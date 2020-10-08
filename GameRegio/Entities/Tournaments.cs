using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Tournaments : MongoDbEntity
    {
        public string GameId { get; set; }
        public int UserCount { get; set; }
        public decimal Reward { get; set; }
        public int GameMinute { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
