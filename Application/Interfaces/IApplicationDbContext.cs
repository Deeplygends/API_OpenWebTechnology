using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<ContactSkill> ContactSkills { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
