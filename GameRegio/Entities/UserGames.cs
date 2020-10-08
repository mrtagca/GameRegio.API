using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class UserGames : MongoDbEntity,IDisposable
    {
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string GameUserName { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
