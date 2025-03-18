using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;

namespace learn_c_sharp.Profiles
{
    public class TouristRoutePictureProfile : Profile
    {
        public TouristRoutePictureProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
        }
    }
}
