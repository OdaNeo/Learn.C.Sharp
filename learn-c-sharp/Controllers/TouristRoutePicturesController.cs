using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;
using learn_c_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace learn_c_sharp.Controllers
{
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    [ApiController]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetPictureListForTouristRoute(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("not found");
            }
            var pictureFromRepo = _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);
            if (pictureFromRepo == null || pictureFromRepo.Count() == 0)
            {
                return NotFound("not found");
            }
            return Ok(_mapper.Map<IEnumerable<TouristRoutePictureDto>>(pictureFromRepo));
        }
        [HttpGet("{pictureId}", Name = "GetPicture")]
        public IActionResult GetPicture(Guid touristRouteId, int pictureId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("not found");
            }
            var pictureFromRepo = _touristRouteRepository.GetPicture(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound("not found");
            }
            return Ok(_mapper.Map<TouristRoutePictureDto>(pictureFromRepo));
        }
        [HttpPost]
        public IActionResult CreateTouristRoutePicture(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRoutePictureFroCreationDto touristRoutePictureFroCreateDto)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("not found");
            }
            var pictureModel = _mapper.Map<TouristRoutePicture>(touristRoutePictureFroCreateDto);
            _touristRouteRepository.AddTouristRoutePicture(touristRouteId, pictureModel);
            _touristRouteRepository.Save();
            var pictureToReturn = _mapper.Map<TouristRoutePictureDto>(pictureModel);
            return CreatedAtRoute(
                "GetPicture", new { touristRouteId = pictureModel.TouristRouteId, pictureId = pictureModel.Id }, pictureToReturn);
        }
        [HttpDelete("{pictureId}")]
        public IActionResult DeletePicture([FromRoute] Guid touristRouteId, [FromRoute] int pictureId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("not found");
            }
            var picture = _touristRouteRepository.GetPicture(pictureId);
            _touristRouteRepository.DeleteTouristRoutePicture(picture);
            _touristRouteRepository.Save();

            return NoContent();
        }
    }
}
