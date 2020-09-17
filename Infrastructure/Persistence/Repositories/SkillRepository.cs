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
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly DbSet<Skill> _skills;
        private readonly DbSet<ContactSkill> _contactSkills;

        public SkillRepository(OWTDbContext dbContext) : base(dbContext)
        {
            _skills = dbContext.Set<Skill>();
            _contactSkills = dbContext.Set<ContactSkill>();
        }

        public override async Task DeleteAsync(Skill skill)
        {
            var linksToDelete = _contactSkills.Where(x => x.IdSkill == skill.Id);
            _contactSkills.RemoveRange(linksToDelete);
            _skills.Remove(skill);
        }
    }
}
