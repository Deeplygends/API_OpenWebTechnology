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

namespace Application.Features.Skill.Commands
{
    public class UpdateSkillCommand : SkillDto, IRequest<Response<UpdateSkillCommand>>
    {
    }

    public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, Response<UpdateSkillCommand>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public UpdateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
        }

        public  async Task<Response<UpdateSkillCommand>> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Domain.Entities.Skill>(request);
            try
            {
                await _skillRepository.UpdateAsync(skill);
                return new Response<UpdateSkillCommand>(request, "Ressource updated", HttpResponseTypeEnum.Created);
            }
            catch (Exception e)
            {
                var failure = new ValidationFailure("Id", "Ressource id not found in the database, has been changed or deleted");
                throw new ValidationException(new List<ValidationFailure>() { failure });
            }
        }
    }
}
