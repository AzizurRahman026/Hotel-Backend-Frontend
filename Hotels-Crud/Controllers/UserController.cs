using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hotels_Crud.Controllers
{
    public class UserController : Controller
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {

            
            return Ok("Hello");
        }
    }
}


