using learn_c_sharp.Dtos;
using learn_c_sharp.Models;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace learn_c_sharp.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticateController(
        IConfiguration configuration,
        UserManager<ApplicationUser> useManager,
        SignInManager<ApplicationUser> signInManager,
        ITouristRouteRepository touristRouteRepository,
        RoleManager<IdentityRole> roleManager
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly UserManager<ApplicationUser> _useManager = useManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ITouristRouteRepository _touristRouteRepository = touristRouteRepository;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private async Task<string> GenerateAccessToken(ApplicationUser user)
        {
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
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }




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

            string accessToken = await GenerateAccessToken(user);

            string refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(5);

            var result = await _useManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                SetRefreshTokenCookie(refreshToken);// 种 token

                return Ok(accessToken);
            }
            else
            {
                return BadRequest(result.Errors);
            }

        }
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            string? refreshToken = HttpContext.Request.Cookies["refreshToken"];
            if (refreshToken.IsNullOrEmpty())
            {
                return BadRequest("Missing refresh token");
            }
            var user = await _useManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
            {
                return Unauthorized("Invalid refresh token");
            }
            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("refreshToken expired");
            }
            var newRefreshToken = GenerateRefreshToken();
            var newAccessToken = await GenerateAccessToken(user);

            user.RefreshToken = newRefreshToken;
            //user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(5);不能一直延长

            var result = await _useManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                SetRefreshTokenCookie(newRefreshToken);// 种 token

                return Ok(newAccessToken);
            }
            else
            {
                return BadRequest(result.Errors);
            }
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

            //绑定Editor权限
            if (!await _roleManager.RoleExistsAsync("Editor"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Editor"));
            }
            var addRoleResult = await _useManager.AddToRoleAsync(user, "Editor");

            if (!addRoleResult.Succeeded)
            {
                return BadRequest(addRoleResult);
            }

            return Ok();
        }
    }
}
