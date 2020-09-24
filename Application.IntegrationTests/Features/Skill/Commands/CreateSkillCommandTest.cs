using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Contacts.Commands;
using Application.Features.Skill.Commands;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Skill.Commands
{
    using static Testing;
    public class CreateSkillCommandTest : TestBase
    {
        [Test]
        public async Task ShouldHaveUniqueNameAndLevel()
        {
            await AddAsync(new Domain.Entities.Skill()
            {
                Name = "SQL",
                Level = SkillLevelEnum.BabyPony.ToString()
            });

            var command = new CreateSkillCommand(){ Name = "SQL", Level = SkillLevelEnum.BabyPony.ToString()};

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldHaveAValidLevelEntry()
        {
            var command = new CreateSkillCommand() {Name = "SQL", Level = "Moyen"};

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateSkill()
        {
            var command = new CreateSkillCommand() {Name = "C#", Level = SkillLevelEnum.Godlike.ToString()};

            var skillId = await SendAsync(command);

            var skill = await FindAsync<Domain.Entities.Skill>(skillId.Data);

            skill.Should().NotBeNull();
            skill.Name.Should().Be(command.Name);
            skill.Level.Should().Be(command.Level);

        }

    }
}
