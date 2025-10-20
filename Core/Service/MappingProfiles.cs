using AutoMapper;
using DomainLayer.Models;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, ProductTypeDTO>();
            CreateMap<ProductBrand, ProductBrandDTO>();
        }
    }
}
