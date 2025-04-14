using AutoMapper;
using learn_c_sharp.Dtos;
using learn_c_sharp.Models;

namespace learn_c_sharp.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<LineItem, LineItemDto>();

        }
    }
}
