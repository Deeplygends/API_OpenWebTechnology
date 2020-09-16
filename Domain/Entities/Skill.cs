using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enum;

namespace Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SkillLevel Level { get; set; }
        public ICollection<ContactSkill> SkillLink { get; set; }
    }
}
