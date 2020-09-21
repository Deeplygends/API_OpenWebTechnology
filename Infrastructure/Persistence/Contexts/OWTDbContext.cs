using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts
{
    public class OWTDbContext : DbContext, IApplicationDbContext
    {
        public OWTDbContext(DbContextOptions<OWTDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ContactSkill>()
                .HasKey(x => new { x.IdContact, x.IdSkill });
            modelBuilder.Entity<ContactSkill>()
                .HasOne(c => c.Contact)
                .WithMany(c => c.ContactLink)
                .HasForeignKey(c => c.IdContact);
            modelBuilder.Entity<ContactSkill>()
                .HasOne(c => c.Skill)
                .WithMany(c => c.SkillLink)
                .HasForeignKey(c => c.IdSkill);
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithOne(u => u.Contact)
                .HasForeignKey<User>(u => u.IdContact);
        }

        #region Entities
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Skill> Skills {get; set;}
        public DbSet<ContactSkill> ContactSkills { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
