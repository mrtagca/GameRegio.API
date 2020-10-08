using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameRegio.Models
{
    public class AuthenticateModel : IDisposable
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
