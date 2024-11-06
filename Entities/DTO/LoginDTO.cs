using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class LoginDTO
    {
        public required string username {get; set; }
        public required string password { get; set; }
    }
}
