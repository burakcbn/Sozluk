using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Api.Application.Features.Command.User.LoginUser
{
    public class LoginUserCommandValidator:AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Email).NotNull()
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("{PropertyName} not a valid email address");

            RuleFor(u => u.Password).NotNull()
                .MinimumLength(6)
                .WithMessage("{PropertyName}should at least be  {MinLenght} characters");
        }
    }
}
