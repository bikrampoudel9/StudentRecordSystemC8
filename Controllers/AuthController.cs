using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using StudentMangementSystemC8.Database.Entities;
using StudentMangementSystemC8.Models;
using StudentMangementSystemC8.Service;

namespace StudentMangementSystemC8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegisterDto dto)
        {
            User user = new User
            {
                UserName = dto.Email!.Split("@")[0],
                FirstName = dto.FirstName!,
                LastName = dto.LastName!,
                Email = dto.Email,
                Address = dto.Address
            };

            var result = await _userManager.CreateAsync(user,dto.Password!);

            if (!result.Succeeded) return BadRequest(result.Errors);
            
            var roleResult = await _userManager.AddToRoleAsync(user,"User");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            
            return Ok("Succesfull Registration with roles");   

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            User? user = await _userManager.FindByEmailAsync(dto.Email!);
            if (user == null) return Unauthorized("Email has not been registered");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password!,true);

            if (result.Succeeded)
            {
                return Ok(

                    new
                    {
                        Message = "Login Success",
                        Token = _jwtService.GenerateToken()
                    }

                    );
            }
            return Unauthorized("Password is not valid");

        }


    }
}
