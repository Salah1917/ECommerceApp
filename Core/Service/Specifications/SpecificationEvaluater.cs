using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public static class SpecificationEvaluater
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;
            if(specifications.Criteria is not null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if(specifications.IncludeExpression is not null && specifications.IncludeExpression.Count > 0)
            {
                Query = specifications.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));
            }
            return Query;
        }
    }
}
