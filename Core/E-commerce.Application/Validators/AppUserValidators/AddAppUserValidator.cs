using E_commerce.Application.ViewModels.AppUserVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Application.Validators.AppUserValidators
{
    public sealed class AddAppUserValidator : AbstractValidator<AppUserAddVM>
    {
        public AddAppUserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name can not be empty")
                .MinimumLength(2)
                .MaximumLength(25)
                .WithMessage("Name length must consist 2-25 characters");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage("Surname can not be empty")
                .Must(surname => !surname.Contains(" "))
                .WithMessage("Surname can not contain white space")
                .MinimumLength(2)
                .MaximumLength(25)
                .WithMessage("Surname length must consist 2-25 characters");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username can not be empty")
                .Must(UserName => !UserName.Contains(" "))
                .WithMessage("Username can not contain white space")
                .MinimumLength(2)
                .MaximumLength(25)
                .WithMessage("Username length must consist 2-25 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("E-Mail con not be empty")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("Password Can not be null")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Must(password => password.Any(char.IsLetter) && password.Any(char.IsDigit))
                .WithMessage("Password mus contain at least 1 letter and 1 digit")
                .Must(password => !password.Contains(" "))
                .WithMessage("Password con not contain white space");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Password and ConfirmPassword not matching");
        }


    }
}
