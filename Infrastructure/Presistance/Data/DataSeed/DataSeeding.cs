using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            #region Automatic Migration

            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }

            #endregion

            #region Seeding Data

            #region Product brand

            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeed\brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                    _dbContext.ProductBrands.AddRange(Brands);
                }
            }

            #endregion

            #region Product type

            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeed\types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                {
                    _dbContext.ProductTypes.AddRange(Types);
                }
            }

            #endregion

            #region Products

            if (!_dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText(@"..\Infrastructure\Presistance\Data\DataSeed\products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    _dbContext.Products.AddRange(Products);
                }
            }

            #endregion

            _dbContext.SaveChanges();

            #endregion

        }
    }
        
}
