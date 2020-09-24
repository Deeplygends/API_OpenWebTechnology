using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Skill.Queries;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Skill.Queries
{
    using static Testing;
    public class GetAllSkillQueryTest : TestBase
    {
        [Test]
        public async Task ShouldGetAllSkills()
        {
            await AddAsync(new Domain.Entities.Skill()
            {
                Name = "Java",
                Level = SkillLevelEnum.Expert.ToString()
            });
            await AddAsync(new Domain.Entities.Skill()
            {
                Name = "C#",
                Level = SkillLevelEnum.Godlike.ToString()
            });
            await AddAsync(new Domain.Entities.Skill()
            {
                Name = "Python",
                Level = SkillLevelEnum.Beginner.ToString()
            });
            await AddAsync(new Domain.Entities.Skill()
            {
                Name = "Assembleur",
                Level = SkillLevelEnum.BabyPony.ToString()
            });

            var query = new GetAllSkillsQuery();

            var response = await SendAsync(query);

            response.Data.Should().NotBeNullOrEmpty();
            response.Data.Should().HaveCount(4);
        }
    }
}
