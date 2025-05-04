using AutoMapper;
using Learn.C.Sharp.Dtos;
using Learn.C.Sharp.Models;

namespace Learn.C.Sharp.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderDto>();
                //.ForMember(
                //    dest => dest.State,
                //    opt =>
                //    {
                //        opt.MapFrom(src => src.State.ToString());
                //    }
                //);
        }
    } 
}
