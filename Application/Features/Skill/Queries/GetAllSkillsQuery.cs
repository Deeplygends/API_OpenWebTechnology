using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Application.Features.Skill.Queries
{
    public class GetAllSkillsQuery: IRequest<Response<IEnumerable<SkillDto>>>
    {
    }

    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillsQuery, Response<IEnumerable<SkillDto>>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetAllSkillQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SkillDto>>> Handle(GetAllSkillsQuery request,
            CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<SkillDto>>(skills);
            return new Response<IEnumerable<SkillDto>>(result, "", HttpResponseTypeEnum.Ok);
        }
    }

    
}
