using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.DataSeed
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            if(_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }

            #region Seeding Data

            #region Product brand



            #endregion

            #endregion
        }
    }
}
