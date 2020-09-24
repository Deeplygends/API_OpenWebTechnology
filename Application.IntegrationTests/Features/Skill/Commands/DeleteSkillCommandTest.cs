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
    public class DeleteSkillCommandTest : TestBase
    {
        [Test]
        public async Task ShouldGiveCorrectIdToDelete()
        {
            await AddAsync(new Domain.Entities.Skill() {Name = "SQL", Level = SkillLevelEnum.Expert.ToString()});

            var command = new DeleteSkillCommand();

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async  Task ShouldDeleteSkill()
        {
            var skillToDelete = await AddAsync(new Domain.Entities.Skill() { Name = "SQL", Level = SkillLevelEnum.Expert.ToString() });

            var command = new DeleteSkillCommand() {Id = skillToDelete.Id};

            await SendAsync(command);

            var skill = await FindAsync<Domain.Entities.Skill>(skillToDelete.Id);

            skill.Should().BeNull();
        }
    }
}
