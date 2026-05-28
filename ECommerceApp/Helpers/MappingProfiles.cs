using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.Basket;
using DomainLayer.Models.OrderAggregate;
using Shared.DTOS;

namespace ECommerceApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductType, ProductTypeDTO>();
            CreateMap<ProductBrand, ProductBrandDTO>();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<DomainLayer.Models.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, DomainLayer.Models.OrderAggregate.Address>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}
