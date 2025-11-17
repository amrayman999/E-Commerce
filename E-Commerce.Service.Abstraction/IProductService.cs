using E_Commerce.Shared.Produucts;

namespace E_Commerce.Service.Abstraction
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();

    }
}
