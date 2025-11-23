using E_Commerce.Domain.Entities.Products;
using E_Commerce.Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Specifications
{
    public class ProductWithBrandTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandTypeSpecification(ProductQueryParameters parameters) 
            : base(CreateCriteria(parameters))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithBrandTypeSpecification(int id)
           : base(x => x.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        private static Expression<Func<Product, bool>> CreateCriteria(ProductQueryParameters parameters)
        {
            return x => (!parameters.BrandId.HasValue || x.BrandId == parameters.BrandId.Value) 
              && (!parameters.TypeId.HasValue || x.BrandId == parameters.TypeId.Value);
        }
    }
}
