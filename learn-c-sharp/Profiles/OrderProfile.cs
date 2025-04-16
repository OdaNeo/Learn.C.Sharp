using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;

namespace learn_c_sharp.Profiles
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
