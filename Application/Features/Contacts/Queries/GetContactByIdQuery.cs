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
using Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Contacts.Queries
{
    public class GetContactByIdQuery : IRequest<Response<ContactDto>>
    {
        public int Id { get; set; }
    }

    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Response<ContactDto>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<ContactDto>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);
           
            if (contact == null)
            {
                var failure = new ValidationFailure("Id", "The id is not present in the database");
                throw new ValidationException(new List<ValidationFailure>() { failure }, HttpResponseTypeEnum.NotFound);
            }
            return new Response<ContactDto>(_mapper.Map<ContactDto>(contact), "", HttpResponseTypeEnum.Ok);

        }
    }
}
