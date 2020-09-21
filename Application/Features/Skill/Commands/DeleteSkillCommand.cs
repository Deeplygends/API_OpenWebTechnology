using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Skill.Commands
{
    public class DeleteSkillCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, Response<int>>
    {
        private readonly ISkillRepository _skillRepository;

        public DeleteSkillCommandHandler(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<Response<int>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _skillRepository.GetByIdAsync(request.Id);
            if (skill == null)
            {
                var failure = new ValidationFailure("Id", "The id is not a valid id, the ressource couldn't be found");
                throw new ValidationException(new List<ValidationFailure>() {failure}, HttpResponseTypeEnum.NotFound);
            }
            await _skillRepository.DeleteAsync(skill);
            return new Response<int>(request.Id, "Ressource Deleted", HttpResponseTypeEnum.Ok);
        }
    }

}
