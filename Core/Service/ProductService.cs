using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var Specifications = new ProductWithBrandAndTypeSpecifications();
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(Specifications);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Products);
        }
        
        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<ProductBrandDTO>>(Brands);
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeDTO>>(Types);
        }

        public async Task<ProductDTO?> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
            return _mapper.Map<Product, ProductDTO>(Product);
        }
    }
}
