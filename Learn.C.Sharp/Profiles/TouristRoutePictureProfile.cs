using AutoMapper;
using Learn.C.Sharp.Dtos;
using Learn.C.Sharp.Models;

namespace Learn.C.Sharp.Profiles
{
    public class TouristRoutePictureProfile : Profile
    {
        public TouristRoutePictureProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
            CreateMap<TouristRoutePictureFroCreationDto, TouristRoutePicture>();
            CreateMap<TouristRoutePicture, TouristRoutePictureFroCreationDto>();
        }
    }
}
