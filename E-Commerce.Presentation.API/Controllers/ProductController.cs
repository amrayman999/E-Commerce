using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Produucts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductController(IProductService productService) : APIBaseController
    {
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return Ok(product);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();
            return Ok(types);
        }
    }
}
