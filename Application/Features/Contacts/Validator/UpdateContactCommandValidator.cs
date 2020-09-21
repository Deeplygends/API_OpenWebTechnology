using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Application.Features.Contacts.Commands;
using Application.Interfaces.Repositories;
using FluentValidation;

namespace Application.Features.Contacts.Validator
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        private readonly IContactRepository _contactRepository;

        public UpdateContactCommandValidator(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("The email doesn't respect the following pattern xxx@yy.zz")
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(c => c.PhoneNumber)
                .Matches(new Regex("^([\\+]?[0-9]{1,3}[\\s.-][0-9]{1,12})([\\s.-]?[0-9]{1,4}?)||([\\+]?[0-9]{1,3}[0-9]{1,12})$")).WithMessage("Enter a valid phone number please (ex : +33606060606 or 0033606060606)");


        }
    }
}
