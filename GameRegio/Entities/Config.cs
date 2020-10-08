using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Config : MongoDbEntity,IDisposable
    {
        public string Brand { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Youtube { get; set; }
        public string Discord { get; set; }
        public string Linkedin { get; set; }
        public string Twitch { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Banner { get; set; }
        public string ApiUrl { get; set; }
        public string ServerIp { get; set; }
        public string OneSignalJson { get; set; }
        public string RelatedJson { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}

