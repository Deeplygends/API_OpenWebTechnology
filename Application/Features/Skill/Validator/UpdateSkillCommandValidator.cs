using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Features.Skill.Commands;
using Application.Interfaces.Repositories;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Skill.Validator
{
    public class UpdateSkillComandValidator : AbstractValidator<UpdateSkillCommand>
    {
        private readonly ISkillRepository _skillRepository;
        private string message;

        public UpdateSkillComandValidator(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
            message = "The Level must be one of the following {";
            Enum.GetNames(typeof(SkillLevelEnum)).ToList().ForEach(x => message += x + ",");
            message += " }";
            RuleFor(x => x.Level).Must(x => Enum.GetNames(typeof(SkillLevelEnum)).ToList().Contains(x)).WithMessage(message);
        }
    }
}
