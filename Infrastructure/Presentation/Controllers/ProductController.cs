using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _serviceManager) : ControllerBase
    {
        #region Get All Products

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            var Products = await _serviceManager.ProductService.GetAllProductsAsync();
            return Ok(Products);
        }

        #endregion

        #region Get Product By Id

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var Product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }
        #endregion

        #region Get All Types

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetTypes()
        {
            var Types = await _serviceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }

        #endregion

        #region Get All Brands

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetBrands()
        {
            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        #endregion
    }
}
