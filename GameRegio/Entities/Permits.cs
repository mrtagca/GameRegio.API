using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Permits : MongoDbEntity,IDisposable
    {
        public string UserId { get; set; }
        public bool EmailPermit { get; set; }
        public bool AppPermit { get; set; }
        public bool WebPermit { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
