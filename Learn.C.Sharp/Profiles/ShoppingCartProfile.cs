using AutoMapper;
using Learn.C.Sharp.Dtos;
using Learn.C.Sharp.Models;

namespace Learn.C.Sharp.Profiles
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
