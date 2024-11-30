using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repositories.Products;
using Repositories.UnitOfWork;
using Services.DTOs;
using Services.ExceptionHandlers;
using Services.Products.ProductRequests;
using Services.Products.ProductResponses;
using System.Net;

namespace Services.Products.ProductServices
{
    public class ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IValidator<CreateProductRequest> createProductRequestValidator, IMapper mapper) : IProductService
    {

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await repository.GetAll().ToListAsync();

            #region Manual Mapping
            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();
            #endregion

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }


        public async Task<ServiceResult<List<ProductDto>>> GetPaginationListAsync(int pageNumber, int pageSize)
        {

            if (pageNumber <= 0)
                return ServiceResult<List<ProductDto>>.Fail("page number cannot be less than 0",HttpStatusCode.BadRequest);

            var products = await repository.GetAll().Skip((pageNumber - 1)*pageSize).Take(10).ToListAsync();

            #region Manual Mapping
            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Stock, p.Price)).ToList();
            //return new ServiceResult<List<ProductDto>> { Data = productAsDto };
            #endregion

            var productsAsDto = mapper.Map<List<ProductDto>>(products);

            return ServiceResult<List<ProductDto>>.Success(productsAsDto);
        }

        public async Task<ServiceResult<List<Product>>> GetTopPriceAsync(int count)
        {
            var products = await repository.GetTopPriceAsync(count);
 

            var productAsDtos = mapper.Map<List<ProductDto>>(products);

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

            var productAsDto = mapper.Map<ProductDto>(product);

            return ServiceResult<ProductDto>.Success(productAsDto!);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
        {

            // 2. async manual service buissness check
            var anyProduct = await repository.GetWhere(x => x.Name == request.Name).AnyAsync();
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Product already exsist", HttpStatusCode.BadRequest);
            }


            // 3. manual FluentValidation buissness check
            //var validationResult = await createProductRequestValidator.ValidateAsync(request);

            //if (!validationResult.IsValid)
            //{
            //    return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(x=>x.ErrorMessage).ToList());
            //}

            var product = mapper.Map<Product>(request);
            await repository.AddAsync(product);
            await unitOfWork.SaveAsync();

            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id.ToString()), $"api/products/{product.Id}");
        }

        public async Task<ServiceResult> UpdateProductAsync(UpdateProductRequest request, string id)
        {

            // Fast Fail

            var product = await repository.GetByIdAsync(Guid.Parse(id));

            if(product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }


            var isProductNameExsist = await repository.GetWhere(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();
            if (isProductNameExsist)
            {
                return ServiceResult.Fail("Product already exsist", HttpStatusCode.BadRequest);
            }

            //product.Name = request.Name;
            //product.Stock = request.Stock;
            //product.Price = request.Price;

            product = mapper.Map(request,product);

            repository.Update(product);
            await unitOfWork.SaveAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await repository.GetByIdAsync(Guid.Parse(request.productId));


            if (product is null)
            {
                return ServiceResult.Fail("Product Not Found", HttpStatusCode.NotFound);
            }

            product.Stock = request.Quantity;
            repository.Update(product);
            await unitOfWork.SaveAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
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
