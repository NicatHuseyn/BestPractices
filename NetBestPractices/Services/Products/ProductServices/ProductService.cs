using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Repositories.GenericRepositories;
using Repositories.Products;
using Repositories.UnitOfWork;
using Services.DTOs;
using Services.Products.ProductRequests;
using Services.Products.ProductResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Services.Products.ProductServices
{
    public class ProductService(IProductRepository repository, IUnitOfWork unitOfWork) : IProductService
    {

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = repository.GetAll();

            var productAsDto = await  products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToListAsync();

            return new ServiceResult<List<ProductDto>> { Data = productAsDto };
        }


        public async Task<ServiceResult<List<ProductDto>>> GetPaginationListAsync(int pageNumber, int pageSize)
        {
            var products = await repository.GetAll().Skip((pageNumber - 1)*pageSize).Take(10).ToListAsync();

            var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();

            return new ServiceResult<List<ProductDto>> { Data = productAsDto };
        }

        public async Task<ServiceResult<List<Product>>> GetTopPriceAsync(int count)
        {
            var products = await repository.GetTopPriceAsync(count);

            var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();


            return new ServiceResult<List<Product>>()
            {
                Data = products
            };
        }

        public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(string id)
        {
            var product = await repository.GetByIdAsync(Guid.Parse(id));

            if (product is null)
            {
                return ServiceResult<ProductDto>.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            var productAsDto = new ProductDto(product!.Id,product.Name,product.Stock,product.Price);

            return ServiceResult<ProductDto>.Success(productAsDto!);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            };
            await repository.AddAsync(product);
            await unitOfWork.SaveAsync();

            return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id.ToString()));
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductRequest request, string id)
        {

            // Fast Fail

            var product = await repository.GetByIdAsync(Guid.Parse(id));
            if(product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;
            repository.Update(product);
            await unitOfWork.SaveAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteProductAsync(string id)
        {
            var product = await repository.GetByIdAsync(Guid.Parse(id));


            if (product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }
            repository.Delete(product);
            await unitOfWork.SaveAsync();
            return ServiceResult.Success();
        }

    }
}
