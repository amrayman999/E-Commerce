using E_Commerce.Service.Abstraction;
using E_Commerce.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductController(IProductService productService) : APIBaseController
    {
        [HttpGet("getallbrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await productService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("getproductbyid/{id}")]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await productService.GetByIdAsync(id);
            return Ok(product);
        }
        [HttpGet("getallproducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync( [FromQuery] ProductQueryParameters parameters)
        {
            var products = await productService.GetProductsAsync(parameters);
            return Ok(products);
        }
        [HttpGet("getalltypes")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await productService.GetTypesAsync();
            return Ok(types);
        }
    }
}
