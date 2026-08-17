using FluentValidation;
using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Validators
{
    public class CreateUserDtoValidtaor :AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidtaor()
        {
            RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}
