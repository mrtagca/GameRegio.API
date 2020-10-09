using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Rule
{
    public class RuleUpdateModel
    {
        public string RuleId { get; set; }
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public string GameId { get; set; }
        public string ModId { get; set; }
        public bool IsActive { get; set; }
    }
}
