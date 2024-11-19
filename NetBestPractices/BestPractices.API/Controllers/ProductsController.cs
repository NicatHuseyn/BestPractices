using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Products.ProductServices;

namespace BestPractices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetTopPriceAsync(3);
            return Ok(products);
        }
    }
}
