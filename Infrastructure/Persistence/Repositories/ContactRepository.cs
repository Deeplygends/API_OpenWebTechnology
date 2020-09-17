using System;
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

        public async Task AddSkillAsync(int idContact, Skill skill)
        {
            var contact = await _contacts.FindAsync(idContact);
            await _contactSkills.AddAsync(new ContactSkill()
                { IdContact = idContact, IdSkill = skill.Id});
            await SaveChangesAsync();

        }

        public override async Task DeleteAsync(Contact contact)
        {
            var linksToDelete = _contactSkills.Where(x => x.IdContact == contact.Id);
            _contactSkills.RemoveRange(linksToDelete);
            _contacts.Remove(contact);
        }

        public async Task RemoveSkillFromContactAsync(int idContact, int idSkill)
        {
            var toDelete = _contactSkills.FirstOrDefault(x => x.IdSkill == idSkill && x.IdContact == idContact);
            if(toDelete != null)
            {
                _contactSkills.Remove(toDelete);
            }
        }
    }
}
