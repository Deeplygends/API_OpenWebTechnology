using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using MediatR;

namespace Application.Features.Contacts.Commands.CreateContact
{

    public partial class CreateContactCommand : ContactDto, IRequest<Response<int>>
    {
    }
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Response<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            await _contactRepository.AddAsync(contact);
            return new Response<int>(contact.Id, "Created", HttpResponseTypeEnum.Created);
        }
    }
}
