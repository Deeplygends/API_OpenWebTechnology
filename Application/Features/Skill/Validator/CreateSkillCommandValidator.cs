using System;
using System.Text.RegularExpressions;
using Application.Features.Skill.Commands;
using Application.Interfaces.Repositories;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Enums;

namespace Application.Features.Skill.Validator
{
    public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
    {
        private readonly ISkillRepository _skillRepository;
        private string message;

        public CreateSkillCommandValidator(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
            message = "The Level must be one of the following {";
            Enum.GetNames(typeof(SkillLevelEnum)).ToList().ForEach(x => message += x + ",");
            message += " }";
            RuleFor(x => x.Level).Must(x => Enum.GetNames(typeof(SkillLevelEnum)).ToList().Contains(x)).WithMessage(message);
        }
    }
}
