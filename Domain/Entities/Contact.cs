using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => LastName + " " + FirstName;

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
