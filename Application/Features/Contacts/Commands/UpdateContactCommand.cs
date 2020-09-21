using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Contacts.Commands
{
    public class UpdateContactCommand : ContactDto, IRequest<Response<UpdateContactCommand>>
    {
    }

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Response<UpdateContactCommand>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateContactCommand>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            try
            {
                await _contactRepository.UpdateAsync(contact);
                return new Response<UpdateContactCommand>(request, "", HttpResponseTypeEnum.NoContent);
            }
            catch (Exception e)
            {
                var failure = new ValidationFailure("Id", "id not found in the database, have been changed or deleted");
                throw new ValidationException(new List<ValidationFailure>() { failure }, HttpResponseTypeEnum.NotFound);
            }
            
        }

    }
}
