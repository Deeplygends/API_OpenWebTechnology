using System;
using System.Collections.Generic;
using System.Text;
using Application.Features.Contacts.Commands.CreateContact;
using Application.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Features.Contacts.Validator
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        private readonly IContactRepository _contactRepository;

        public CreateContactCommandValidator(IContactRepository contactRepository)
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

        }
    }
}
