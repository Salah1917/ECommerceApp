using Shared;
using Shared.DTOS;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParams queryParams);

        Task<ProductDTO?> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync();
    }
}
