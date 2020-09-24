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

namespace Application.Features.Contacts.Queries
{
    public class GetSkillsOfContactQuery : IRequest<Response<IEnumerable<SkillDto>>>
    {
        public int Id { get; set; }
    }
    public class GetSkillsOfContactQueryHandler : IRequestHandler<GetSkillsOfContactQuery, Response<IEnumerable<SkillDto>>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;
        
        public GetSkillsOfContactQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _mapper = mapper;
            _skillRepository = skillRepository;
        }

        public async Task<Response<IEnumerable<SkillDto>>> Handle(GetSkillsOfContactQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetSkillsByContactId(request.Id);
            var skillDto = _mapper.Map<IEnumerable<SkillDto>>(skills);
            return new Response<IEnumerable<SkillDto>>(skillDto);
        }
    }
}
