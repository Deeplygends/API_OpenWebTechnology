using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly DbSet<Contact> _contacts;

        public ContactRepository(OWTDbContext dbContext) : base(dbContext)
        {
            _contacts = dbContext.Set<Contact>();
        }
    }
}
