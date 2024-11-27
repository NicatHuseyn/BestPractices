using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Products.ProductRequests;
using Services.Products.ProductServices;
using System.Net;
using System.Runtime.InteropServices.Marshalling;

namespace BestPractices.API.Controllers
{
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllListAsync());

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPaginationListAsync(pageNumber,pageSize));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => CreateActionResult(await productService.GetProductByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request) => CreateActionResult(await productService.CreateProductAsync(request));

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(UpdateProductRequest request, string id) => CreateActionResult(await productService.UpdateProductAsync(request, id));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(string id) => CreateActionResult(await productService.DeleteProductAsync(id));
    }
}
