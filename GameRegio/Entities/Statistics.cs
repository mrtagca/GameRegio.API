using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Statistics : MongoDbEntity
    {
        public string UserId { get; set; }
        public string GameId { get; set; }
        public int WinCount { get; set; }
        public int LostCount { get; set; }

    }
}
