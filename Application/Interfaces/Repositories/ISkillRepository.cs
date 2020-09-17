using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task<IReadOnlyList<Skill>> GetSkillsByContactId(int id);
    }
}
