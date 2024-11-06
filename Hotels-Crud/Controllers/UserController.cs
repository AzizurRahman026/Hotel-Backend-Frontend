using System.Security.Cryptography;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Hotels_Crud.DTO;
using Entities.DTO;

namespace Hotels_Crud.Controllers
{
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO user)
        {
            Console.WriteLine(user.username + " " + user.password);
            if (await UserExist(user.username))
            {
                return BadRequest("User already Exists");
            }
            return Ok(await _userServices.RegisterUser(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!await UserExist(loginDto.username))
            {
                return Unauthorized("Invalid username");
            }
            var user = await _userServices.Login(loginDto);
            Console.WriteLine("User Details: " + user + "\n\n");
            if (user == null)
            {
                return Unauthorized("Invalid password");
            }
            return Ok(user);
        }










        
        // private function...
        private async Task<bool> UserExist(string username)
        {
            bool haveUser = await _userServices.UserExist(username);
            if (haveUser)
            {
                return true;
            }
            return false;
        }
    }
}


