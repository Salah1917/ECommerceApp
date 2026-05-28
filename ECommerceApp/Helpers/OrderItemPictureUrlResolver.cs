using AutoMapper;
using DomainLayer.Models.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;

namespace ECommerceApp.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;

            return $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
