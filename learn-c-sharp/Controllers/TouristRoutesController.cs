using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;
using learn_c_sharp.ResourceParameters;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace learn_c_sharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [HttpHead]
        public IActionResult GetTouristRoutes([FromQuery] TouristRouteResoureceParamaters paramates)//Core 3.x 以上，需要加问号
        {

            var touristRoutesFromRepo = _touristRouteRepository.GetTouristRoutes(paramates.Keyword, paramates.RatingOperator, paramates.RatingValue);
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("not found");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);
            return Ok(touristRoutesDto);
        }
        [HttpGet("{touristRoueId}", Name = "GetTouristRouteById")]
        [HttpHead]
        public IActionResult GetTouristRouteById(Guid touristRoueId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRoueId);
            if (touristRouteFromRepo == null)
            {
                return NotFound("not found");
            }
            //var touristRouteDto = new TouristRouteDto()
            //{
            //    Id = touristRouteFromRepo.Id,
            //    Title = touristRouteFromRepo.Title,
            //    Description = touristRouteFromRepo.Description,
            //    Price = touristRouteFromRepo.OriginalPrice * (decimal)(touristRouteFromRepo.DiscountPresent ?? 1),
            //    CreateTime = touristRouteFromRepo.CreateTime,
            //    UpdateTime = touristRouteFromRepo.UpdateTime,
            //    Features = touristRouteFromRepo.Features,
            //    Fees = touristRouteFromRepo.Fees,
            //    Notes = touristRouteFromRepo.Notes,
            //    Rating = touristRouteFromRepo.Rating,
            //    TravelDays = touristRouteFromRepo.TravelDays.ToString(),
            //    TripType = touristRouteFromRepo.TripType.ToString(),
            //    DepartureCity = touristRouteFromRepo.DepartureCity.ToString(),
            //};
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
            return Ok(touristRouteDto);
        }
        [HttpPost]
        public IActionResult CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationDto)
        {
            var touristRouteModel = _mapper.Map<TouristRoute>(touristRouteForCreationDto);
            _touristRouteRepository.AddTouristRoute(touristRouteModel);
            _touristRouteRepository.Save();
            var touristRouteToReturn = _mapper.Map<TouristRouteDto>(touristRouteModel);
            return CreatedAtRoute("GetTouristRouteById", new { touristRoueId = touristRouteToReturn.Id }, touristRouteToReturn);

        }
        [HttpPut("{touristRoueId}")]
        public IActionResult UpdateTouristRoute([FromRoute] Guid touristRoueId, [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRoueId))
            {
                return NotFound("not found");
            }
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRoueId);
            // 映射Dto
            // 更新Dto
            // 映射model
            _mapper.Map(touristRouteForUpdateDto, touristRouteFromRepo);
            _touristRouteRepository.Save();
            return NoContent();
        }
        [HttpPatch("{touristRoueId}")]
        public IActionResult PartiallyUpdateTouristRoute(
            [FromRoute] Guid touristRoueId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> jsonPatchDocument)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRoueId))
            {
                return NotFound("not found");
            }
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRoueId);
            var touristRouteToPatch = _mapper.Map<TouristRouteForUpdateDto>(touristRouteFromRepo);
            jsonPatchDocument.ApplyTo(touristRouteToPatch, ModelState);
            if (!TryValidateModel(touristRouteToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(touristRouteToPatch, touristRouteFromRepo);
            _touristRouteRepository.Save();

            return NoContent();
        }
    }
}
