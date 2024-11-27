using Repositories.Products;
using Services.DTOs;
using Services.Products.ProductRequests;
using Services.Products.ProductResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Products.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
        Task<ServiceResult<List<ProductDto>>> GetPaginationListAsync(int pageNumber, int pageSize);
        Task<ServiceResult<List<Product>>> GetTopPriceAsync(int count);
        Task<ServiceResult<ProductDto>> GetProductByIdAsync(string id);
        Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateProductAsync(UpdateProductRequest request, string id);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
        Task<ServiceResult> DeleteProductAsync(string id);
    }
}
