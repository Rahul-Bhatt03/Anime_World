using AnimeWorld.Interfaces;
using AnimeWorld.Model.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimeWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userService.RegisterAsync(registerUserDto);
            if (user == null) {
                return BadRequest("User already exists");
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) {
            var user = await _userService.LoginAsync(loginDto);
            if (user == null)
            {
                return Unauthorized("invalid credentials");
            }
            var token = CreateToken(user);
            return Ok(token);
        }

        [HttpPatch("updateUser/{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UserDto request)
        {
            var user = await _userService.UpdateAsync(id, request);
            if (user == null)
            {
                return NotFound("user not found");
            }
            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult>DeleteUser(int id)
        {
            var user= await _userService.DeleteAsync(id);
            if (user == null)
            {
                return NotFound("user not found");
            }
            return NoContent();
        }

        [HttpGet("GetAllUsersDetails")]
        public async Task<ActionResult> GetAllUsersDetails()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("GetUserDetailsById/{id}")]
        public async Task<ActionResult>GetUserDetailsById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound("User not found");
            return Ok(user);
        }

        private string CreateToken(UserDto user) {
            var claims = new List<Claim>
            {
                 new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
