
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Skill.Commands;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Skill.Commands
{
    using static Testing;
    public class UpdateSkillCommandTest : TestBase
    {
        [Test]
        public async Task ShouldHaveAValidLevelEntry()
        {
            var skill = await AddAsync(new Domain.Entities.Skill()
            {
                Name = "Java",
                Level = SkillLevelEnum.Expert.ToString()
            });

            var command = new UpdateSkillCommand() {Id = skill.Id, Name = "Kobol", Level = "Chosen One"};

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();

        }

        [Test]
        public async Task ShouldUpdateSkill()
        {

        }
    }
}
