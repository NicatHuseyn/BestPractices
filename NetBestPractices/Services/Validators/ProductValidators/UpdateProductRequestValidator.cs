﻿using FluentValidation;
using Services.Products.ProductRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validators.ProductValidators
{
    public class UpdateProductRequestValidator:AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            // Product name validator
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter your product name")
                .NotNull().WithMessage("Please enter your product name")
                .Length(1, 15).WithMessage("Product name character count between 3 and 10");

            //sync valid
            //.Must(MustUniqueProductName).WithMessage("product name is in the database"); 

            // async validation
            //.MustAsync(MustBeUniqueProductNameAsync).WithMessage("product name is in the database");



            // Product price validator
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("product price must be greater than 0");

            // Product stock validator
            RuleFor(x => x.Stock)
                .InclusiveBetween(1, 500).WithMessage("Product stock should be between 1, 100");
        }
    }
}
