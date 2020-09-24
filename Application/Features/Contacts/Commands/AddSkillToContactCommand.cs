using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using Application.Features.Skill.Commands;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Contacts.Commands
{
    public class AddSkillToContactCommand : SkillContactDto, IRequest<Response<int>>
    {
    }

    public class AddSkillToContactCommandHandler : IRequestHandler<AddSkillToContactCommand, Response<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddSkillToContactCommandHandler(IContactRepository contactRepository, ISkillRepository skillRepository, IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _skillRepository = skillRepository;
            _mediator = mediator;
        }

        public async Task<Response<int>> Handle(AddSkillToContactCommand request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAllAsync();
            var skill = skills.ToList()
                .FirstOrDefault(x => x.Name == request.Skill.Name && x.Level == request.Skill.Level);
            int id = 0;
            var responseType = HttpResponseTypeEnum.Ok;
            if (skill == null)
            {
                var command = _mapper.Map<CreateSkillCommand>(request.Skill);
                var response  = await _mediator.Send(command);
                if (response.Succeeded == false)
                    return response;
                id = response.Data;
                responseType = HttpResponseTypeEnum.Created;
            }
            else
            {
                id = skill.Id;
            }
            await _contactRepository.AddSkillAsync(request.IdContact, id);
            return new Response<int>(id, $"Skill {request.Skill.Name} - {request.Skill.Level} has been add to contact {request.IdContact}", responseType);
        }
    }
}
