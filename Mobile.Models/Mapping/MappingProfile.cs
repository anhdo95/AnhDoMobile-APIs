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

            CreateMap<Order, OrderCompleteViewModel>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ShipPhone, opt => opt.MapFrom(src => src.ShipMobile))
                .ForMember(dest => dest.OrderTotal, opt => opt.MapFrom(src => src.Total));

            CreateMap<OrderDetail, CompleteProductViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));
        }
    }
}
