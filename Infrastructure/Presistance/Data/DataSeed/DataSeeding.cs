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
        public async Task DataSeedAsync()
        {
            #region Automatic Migration

            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
            {
                _dbContext.Database.Migrate();
            }

            #endregion

            #region Seeding Data

            #region Product brand

            if (!_dbContext.ProductBrands.Any())
            {
                var BrandsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\brands.json");
                var Brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(BrandsData);
                if (Brands is not null && Brands.Any())
                {
                    await _dbContext.ProductBrands.AddRangeAsync(Brands);
                }
            }

            #endregion

            #region Product type

            if (!_dbContext.ProductTypes.Any())
            {
                var TypesData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\types.json");
                var Types = await JsonSerializer.DeserializeAsync<List<ProductType>>(TypesData);
                if (Types is not null && Types.Any())
                {
                    await _dbContext.ProductTypes.AddRangeAsync(Types);
                }
            }

            #endregion

            #region Products

            if (!_dbContext.Products.Any())
            {
                var ProductsData = File.OpenRead(@"..\Infrastructure\Presistance\Data\DataSeed\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if (Products is not null && Products.Any())
                {
                    await _dbContext.Products.AddRangeAsync(Products);
                }
            }

            #endregion

            await _dbContext.SaveChangesAsync();

            #endregion

        }
    }
        
}
