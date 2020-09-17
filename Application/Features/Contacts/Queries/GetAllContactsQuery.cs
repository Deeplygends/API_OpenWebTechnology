using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Contacts.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<Response<IEnumerable<ContactDto>>>
    {
    }

    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, Response<IEnumerable<ContactDto>>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetAllContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ContactDto>>> Handle(GetAllContactsQuery request,
            CancellationToken cancellationToken)
        {
            var contacts = await _contactRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ContactDto>>(contacts);
            return new Response<IEnumerable<ContactDto>>(result, "", HttpResponseTypeEnum.Ok); 
        }
    }
}
