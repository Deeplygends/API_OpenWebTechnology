using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Wrapper;
using Domain.Enums;

namespace Application.Features.Skill.Commands
{
    public class GetAllSkillLevelQuery : IRequest<Response<IEnumerable<string>>>
    {
    }

    public class GetAllSkillLevelQueryHandler : IRequestHandler<GetAllSkillLevelQuery, Response<IEnumerable<string>>>
    {
        public GetAllSkillLevelQueryHandler() {}

        public async Task<Response<IEnumerable<string>>> Handle(GetAllSkillLevelQuery request,
            CancellationToken cancellationToken)
        {
            var levels = Enum.GetNames(typeof(SkillLevelEnum)).ToList();
            return new Response<IEnumerable<string>>(levels, "", HttpResponseTypeEnum.Ok);
        }

    }
}
