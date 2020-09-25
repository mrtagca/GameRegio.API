using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Wallets : MongoDbEntity
    {
        public string UserId { get; set; }
        public double CurrentBalance { get; set; }
        public string PaparaId { get; set; }
    }
}
