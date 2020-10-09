using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.ServiceContracts.Permit
{
    public class PermitUpdateModel
    {
        public string PermitId { get; set; }
        public string UserId { get; set; }
        public bool EmailPermit { get; set; }
        public bool AppPermit { get; set; }
        public bool WebPermit { get; set; }
    }
}
