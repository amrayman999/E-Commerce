using E_Commerce.Shared.Products;

namespace E_Commerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();

    }
}
