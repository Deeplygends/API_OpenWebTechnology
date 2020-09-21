using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.Features.Contacts.Commands
{
    public class RemoveSkillFromContactCommand : IRequest<Response<int>>
    {
        public int IdContact { get; set; }
        public int IdSkill { get; set; }

    }
    public class RemoveSkillFromContactCommandHandler : IRequestHandler<RemoveSkillFromContactCommand, Response<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public RemoveSkillFromContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<Response<int>> Handle(RemoveSkillFromContactCommand request,
            CancellationToken cancellationToken)
        {
            await _contactRepository.RemoveSkillFromContactAsync(request.IdContact, request.IdSkill);
            return new Response<int>(1, "Skill removed", HttpResponseTypeEnum.Ok);
        }
    }
}
