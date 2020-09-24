using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests
{
    public class SkillUnitTest
    {
        private Skill _skill;

        public SkillUnitTest()
        {
            _skill = new Skill()
            {
                Name = "C#",
                Level = "Expert"
            };
        }

        [Fact]
        public void GetNameTest()
        {
            _skill.Name.Should().BeEquivalentTo("C#");
        }
        [Fact]
        public void GetLevelTest()
        {
            _skill.Level.Should().BeEquivalentTo("Expert");
        }
        [Fact]
        public void SetNameTest()
        {
            _skill.Name = "SQL";
            _skill.Name.Should().BeEquivalentTo("SQL");
        }
        [Fact]
        public void SetLevelTest()
        {
            _skill.Level = "GodLike";
            _skill.Level.Should().BeEquivalentTo("GodLike");
        }
    }
}
