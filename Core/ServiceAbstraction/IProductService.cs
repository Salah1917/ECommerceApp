using Shared.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();

        Task<ProductDTO?> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync();

        Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync();
    }
}
