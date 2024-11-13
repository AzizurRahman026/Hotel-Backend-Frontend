using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Hotels_Crud.DTO;
using Entities.DTO;
using IServices;
using Microsoft.AspNetCore.Authorization;

namespace Hotels_Crud.Controllers
{
    [ApiController]
    [Authorize]  // Apply to all actions in this controller
    public class UserController : Controller
    {

        private readonly IUserServices _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserServices userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> getUser(string id)
        {
            try
            {
                var getUser = await _userService.getUser(id);
                if (getUser == null)
                {
                    return BadRequest("User not found...");
                }
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("user/register")]
        [AllowAnonymous]  // Allow anonymous access to registration
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO user)
        {
            Console.WriteLine(user.username + " " + user.password);
            try
            {
                if (await UserExist(user.username))
                {
                    return BadRequest("User already Exists");
                }

                var curUser = await _userService.RegisterUser(user);

                var userdto = new UserDto
                {
                    Username = user.username,
                    Token = _tokenService.CreateToken(curUser)
                };

                return Ok(userdto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user/login")]
        [AllowAnonymous]  // Allow anonymous access to login
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            Console.WriteLine(loginDto.username + " | " + loginDto.password);
            if (!await UserExist(loginDto.username))
            {
                return Unauthorized("Invalid username");
            }
            var curUser = await _userService.Login(loginDto);
            Console.WriteLine("User Details: " + curUser + "\n\n");
            if (curUser == null)
            {
                return Unauthorized("Invalid password");
            }

            var userdto = new UserDto
            {
                Username = curUser.Username,
                Token = _tokenService.CreateToken(curUser)
            };

            return Ok(userdto);
        }
        
        // private function...
        private async Task<bool> UserExist(string username)
        {
            bool haveUser = await _userService.UserExist(username);
            if (haveUser)
            {
                return true;
            }
            return false;
        }
    }
}


