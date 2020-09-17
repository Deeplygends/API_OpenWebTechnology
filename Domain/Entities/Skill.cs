using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public ICollection<ContactSkill> SkillLink { get; set; }
    }
}
