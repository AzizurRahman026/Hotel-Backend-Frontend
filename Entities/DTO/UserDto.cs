using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
    }
}
