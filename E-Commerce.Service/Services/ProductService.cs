using AutoMapper;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared.Products;

namespace E_Commerce.Service.Services
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return  mapper.Map<IEnumerable<BrandDto>>(brands);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var specs = new ProductWithBrandTypeSpecification(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(specs);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var specs = new ProductWithBrandTypeSpecification(parameters);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            return mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeDto>>(types);
        }
    }
}
