using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
