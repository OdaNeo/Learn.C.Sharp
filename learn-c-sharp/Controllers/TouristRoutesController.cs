using learn_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace learn_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository)
        {
            _touristRouteRepository = touristRouteRepository;
        }
        [HttpGet]
        public IActionResult GetTouristRoutes() 
        {
            var touristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes();
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0) 
            {
                return NotFound("not found");
            }
            return Ok(touristRoutesFromRepo);
        }
        [HttpGet("{touristRoueId}")]
        public IActionResult GetTouristRouteById(Guid touristRoueId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRoueId);
            if (touristRouteFromRepo == null)
            {
                return NotFound("not found");
            }
            return Ok(touristRouteFromRepo);
        }
    }
}
