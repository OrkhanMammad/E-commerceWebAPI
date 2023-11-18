using E_commerce.Application.ViewModels.ProductVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Validators.ProductValidators
{
    public sealed class AddProductValidator : AbstractValidator<ProductAddVM>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .WithMessage("Name could not be empty")
               .MaximumLength(100)
               .WithMessage("Name must be max 100 char")
               .MinimumLength(3)
               .WithMessage("Name must be min 3 char");



            RuleFor(x => x.Description)
               .NotEmpty()
               .NotNull()
               .WithMessage("Name could not be empty")
               .MaximumLength(100)
               .WithMessage("Name must be max 100 char")
               .MinimumLength(3)
               .WithMessage("Name must be min 3 char");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stock could not be empty")
                .Must(s => s > 0)
                .WithMessage("Stock can not be negative or 0");

            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                .WithMessage("Price could not be empty")
                .Must(s => s > 0)
                .WithMessage("Price can not be negative or 0");

            RuleFor(x => x.Images)
                .NotEmpty()
                .NotNull()
                .WithMessage("Images can not be empty");
        }



    }
}
