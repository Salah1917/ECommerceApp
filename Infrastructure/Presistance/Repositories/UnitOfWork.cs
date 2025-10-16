using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var TypeName = typeof(TEntity).Name;

            //Dictionary<string, object> string -> Repository Name and Object is the Repo itself
            //if(_Repositories.ContainsKey(TypeName))
            //{
            //    return (IGenericRepository<TEntity, TKey>)_Repositories[TypeName];
            //}

            if(_Repositories.TryGetValue(TypeName, out object? value))
            {
                 return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                //Create the object
                var Repository = new GenericRepository<TEntity, TKey>(_dbContext);

                //Store Object In Dictionary
                //_Repositories.Add(TypeName, Repository);
                _Repositories[TypeName] = Repository;

                //Return Object
                return Repository;

            }


        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
