using E_Commerce.Presentation.API.Attributes;
using E_Commerce.Service.Abstraction;
using E_Commerce.Service.Abstraction.Products;
using E_Commerce.Shared;
using E_Commerce.Shared.Dtos.Products;
using E_Commerce.Shared.ErrorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Presentation.API.Controllers
{
    public class ProductsController(IServiceManager _serviceManager) : APIBaseController
    {
        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrandsAsync()
        {
            var brands = await _serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
        {
            var product = await _serviceManager.ProductService.GetByIdAsync(id);
            return Ok(product);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResult<ProductDto>))]
        [Cache(50)]
        [Authorize]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetProductsAsync( [FromQuery] ProductQueryParameters parameters)
        {
            var products = await _serviceManager.ProductService.GetProductsAsync(parameters);
            return Ok(products);
        }
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypesAsync()
        {
            var types = await _serviceManager.ProductService.GetTypesAsync();
            return Ok(types);
        }
    }
}
