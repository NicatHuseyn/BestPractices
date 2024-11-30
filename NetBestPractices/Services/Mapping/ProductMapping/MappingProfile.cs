using AutoMapper;
using Repositories.Categories;
using Repositories.Products;
using Services.Categories.CategoryRequests;
using Services.DTOs;
using Services.Products.ProductRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping.ProductMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mappings
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<CreateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

            CreateMap<UpdateProductRequest, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
            #endregion
        }
    }
}
