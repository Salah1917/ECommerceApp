using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PictureUrlResolver(IConfiguration Configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                var Url = $"{Configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
                return Url;
            }
        }
    }
}
