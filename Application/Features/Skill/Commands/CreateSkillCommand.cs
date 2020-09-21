using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.EntityFrameworkCore.Internal;

namespace Application.Features.Skill.Commands
{
    public class CreateSkillCommand : SkillDto, IRequest<Response<int>>
    {
    }

    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Response<int>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
        }
        public async Task<Response<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Domain.Entities.Skill>(request);
            var skills = await _skillRepository.GetAllAsync();
            if (skills.ToList().Any(x => x.Name.ToUpper() == skill.Name.ToUpper() && x.Level == skill.Level))
            {
                var failure = new ValidationFailure("AlreadyExist", "The ressource already exists in the database");
                throw new ValidationException(new List<ValidationFailure>() { failure }, HttpResponseTypeEnum.Conflict);
            }
            await _skillRepository.AddAsync(skill);
            return new Response<int>(skill.Id, "Created", HttpResponseTypeEnum.Created);
        }
    }
}
