using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => LastName + " " + FirstName;

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ContactSkill> ContactLink { get; set; }
        public User User { get; set; }
    }
}
