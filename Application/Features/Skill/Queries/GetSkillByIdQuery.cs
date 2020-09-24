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

namespace Application.Features.Skill.Queries
{
    public class GetSkillByIdQuery : IRequest<Response<SkillDto>>
    {
        public int Id { get; set; }
    }

    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, Response<SkillDto>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillByIdQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
        }

        public async Task<Response<SkillDto>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id);
            if (skill == null)
            {
                var failure = new ValidationFailure("id", "The id is not a valid id");
                throw new ValidationException(new List<ValidationFailure>(){failure}, HttpResponseTypeEnum.NotFound);
            }
            return new Response<SkillDto>(_mapper.Map<SkillDto>(skill), "", HttpResponseTypeEnum.Ok);
        }
    }
}
