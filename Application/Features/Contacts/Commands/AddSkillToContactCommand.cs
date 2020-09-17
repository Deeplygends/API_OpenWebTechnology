using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using MediatR;

namespace Application.Features.Contacts.Commands
{
    public class AddSkillToContactCommand : SkillContactDto, IRequest<Response<int>>
    {
    }

    public class AddSkillToContactCommandHandler : IRequestHandler<AddSkillToContactCommand, Response<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public AddSkillToContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<Response<int>> Handle(AddSkillToContactCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Domain.Entities.Skill>(request.Skill);
            await _contactRepository.AddSkillAsync(request.IdContact, skill );
            return new Response<int>(1, $"Skill {request.Skill.Name} - {request.Skill.Level} has been add to contact {request.IdContact}");
        }
    }
}
