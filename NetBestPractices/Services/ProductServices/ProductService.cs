﻿using Repositories.GenericRepositories;
using Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductServices
{
    public class ProductService(IProductRepository repository):IProductService
    {

    }
}