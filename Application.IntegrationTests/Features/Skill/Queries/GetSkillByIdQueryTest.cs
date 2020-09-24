using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Skill.Queries;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Skill.Queries
{
    using static Testing;
    public class GetSkillByIdQueryTest : TestBase
    {
        [Test]
        public async Task ShouldGiveAValidId()
        {
            var addSkill = await AddAsync(new Domain.Entities.Skill()
            {
                Name = "C++",
                Level = SkillLevelEnum.Intermediate.ToString()
            });

            var query = new GetSkillByIdQuery();

            FluentActions.Invoking(() => SendAsync(query)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldGiveTheSkillById()
        {
            var addSkill = await AddAsync(new Domain.Entities.Skill()
            {
                Name = "C++",
                Level = SkillLevelEnum.Intermediate.ToString()
            });

            var query = new GetSkillByIdQuery() {Id = addSkill.Id};

            var result = await SendAsync(query);

            result.Data.Should().NotBeNull();
            result.Data.Level.Should().Be(addSkill.Name);
            result.Data.Name.Should().Be(addSkill.Name);
        }
    }
}
