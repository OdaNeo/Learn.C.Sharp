using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace learn_c_sharp.Controllers
{
    [ApiController]
    [Route("api/shoppingCart")]
    public class ShoppingCartController(IHttpContextAccessor httpContextAccessor, ITouristRouteRepository touristRouteRepository, IMapper mapper) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITouristRouteRepository _touristRouteRepository = touristRouteRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetShoppingCart()
        {
            // 获取当前用户
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shoppingCart = await _touristRouteRepository.GetShoppingCartByUserId(userId);

            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }
        [HttpPost("items")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddShoppingCartItem([FromBody] AddShoppingCartItemDto addShoppingCartItemDto)
        {
            // 获取当前用户
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var shoppingCart = await _touristRouteRepository.GetShoppingCartByUserId(userId);

            var touristRoute = await _touristRouteRepository.GetTouristRouteAsync(addShoppingCartItemDto.TouristRouteId);
            if (touristRoute == null)
            {
                return NotFound("not found");
            }
            var lineItem = new LineItem()
            {
                TouristRouteId = addShoppingCartItemDto.TouristRouteId,
                ShoppingCartId = shoppingCart.Id,
                OriginalPrice = touristRoute.OriginalPrice,
                DiscountPresent = touristRoute.DiscountPresent,
            };
            await _touristRouteRepository.AddShoppingCartItem(lineItem);
            await _touristRouteRepository.SaveAsync();

            return Ok(_mapper.Map<ShoppingCartDto>(shoppingCart));
        }
    }
}
