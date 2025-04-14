using learn_c_sharp.Dtos;
using learn_c_sharp.Models;
using learn_c_sharp.Services;
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
    public class AuthenticateController(
        IConfiguration configuration,
        UserManager<ApplicationUser> useManager,
        SignInManager<ApplicationUser> signInManager,
        ITouristRouteRepository touristRouteRepository
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<ApplicationUser> _useManager = useManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ITouristRouteRepository _touristRouteRepository = touristRouteRepository;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!loginResult.Succeeded)
            {
                return BadRequest(loginResult);
            }

            var user = await _useManager.FindByNameAsync(loginDto.Email);

            var signingAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user!.Id),
                //new Claim(ClaimTypes.Role,"Admin")
            };
            var roleNames = await _useManager.GetRolesAsync(user);
            foreach (var roleName in roleNames)
            {
                var roleClaim = new Claim(ClaimTypes.Role, roleName);
                claims.Add(roleClaim);
            }

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
            var user = new ApplicationUser()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email
            };
            var result = await _useManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            //初始化购物车
            var shoppingCart = new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
            };
            await _touristRouteRepository.CreateShoppingCart(shoppingCart);
            await _touristRouteRepository.SaveAsync();

            return Ok();
        }
    }
}
