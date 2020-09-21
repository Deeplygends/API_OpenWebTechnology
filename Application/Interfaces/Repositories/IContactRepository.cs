using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task AddSkillAsync(int idContact, int idskill);
        Task RemoveSkillFromContactAsync(int idContact, int idSkill);
    }
}
