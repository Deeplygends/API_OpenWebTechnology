using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ContactSkill
    {
        public int IdContact { get; set; }
        public int IdSkill { get; set; }

        public Skill Skill { get; set; }
        public Contact Contact { get; set; }
    }
}
