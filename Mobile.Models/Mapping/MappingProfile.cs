using AutoMapper;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;

namespace Mobile.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cart, CartItemViewModel>()
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductMetaTitle, opt => opt.MapFrom(src => src.Product.MetaTitle))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}
