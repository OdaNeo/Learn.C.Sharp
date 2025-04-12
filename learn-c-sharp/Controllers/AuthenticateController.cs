using learn_c_sharp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace learn_c_sharp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticateController(IConfiguration configuration, UserManager<IdentityUser> useManager) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<IdentityUser> _useManager = useManager;

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login([FromBody] LoginDto loginDto)
        {
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"fake_user_id"),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!);
            var signingKey = new SymmetricSecurityKey(secretByte);
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenStr);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new IdentityUser()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var result = await _useManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok();
        }
    }
}
