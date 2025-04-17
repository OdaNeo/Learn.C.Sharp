using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace learn_c_sharp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController(IHttpContextAccessor httpContextAccessor, ITouristRouteRepository touristRouteRepository, IMapper mapper) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITouristRouteRepository _touristRouteRepository = touristRouteRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrders()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _touristRouteRepository.GetOrdersByUserId(userId);

            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
            //return Ok(orders);
        }
        [HttpGet("{orderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var order = await _touristRouteRepository.GetOrderById(orderId);

            return Ok(_mapper.Map<OrderDto>(order));
        }
    }
}
