using GameRegio.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Entities
{
    public class Rules : MongoDbEntity
    {
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public string GameId { get; set; }
        public string ModId { get; set; }
        public bool IsActive { get; set; }

    }
}
