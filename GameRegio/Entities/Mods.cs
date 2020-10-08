using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Mods : MongoDbEntity
    {
        public string ModName { get; set; }
        public string GameId { get; set; }

    }
}
