﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly DbSet<Contact> _contacts;
        private readonly DbSet<ContactSkill> _contactSkills;

        public ContactRepository(OWTDbContext dbContext) : base(dbContext)
        {
            _contacts = dbContext.Set<Contact>();
            _contactSkills = dbContext.Set<ContactSkill>();
        }

        public override async Task DeleteAsync(Contact contact)
        {
            var linksToDelete = _contactSkills.Where(x => x.IdContact == contact.Id);
            _contactSkills.RemoveRange(linksToDelete);
            _contacts.Remove(contact);
        }
    }
}
