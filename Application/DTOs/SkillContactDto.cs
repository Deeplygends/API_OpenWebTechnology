using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.DTOs
{
    public class SkillContactDto
    {
        public int IdContact { get; set; }

        public SkillDto Skill { get; set; }
    }
}
