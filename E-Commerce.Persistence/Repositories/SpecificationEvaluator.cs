using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public static class SpecificationEvaluator 
    {
        public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery, 
                                           ISpecification<TEntity> specification) where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            if(specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }
            query = specification.Includes.Aggregate(query, (query, expression) => query.Include(expression));
            return query;
        }
    }
}
