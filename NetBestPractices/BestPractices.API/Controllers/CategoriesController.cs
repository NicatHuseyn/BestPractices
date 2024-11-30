using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Categories.CategoryRequests;
using Services.Categories.CategoryServices;
using Services.Products.ProductRequests;
using Services.Products.ProductServices;

namespace BestPractices.API.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await categoryService.GetAllCategoryListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => CreateActionResult(await categoryService.GetByIdCategory(id));

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(string id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts() => CreateActionResult(await categoryService.GetAllCategoryListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequest request) => CreateActionResult(await categoryService.CreateAsync(request));

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(UpdateCategoryRequest request, string id) => CreateActionResult(await categoryService.UpdateAsync(request, id));

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(string id) => CreateActionResult(await categoryService.DeleteAsync(id));
    }
}
