using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Features.User.Command;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> Authentificate(AuthentificationCommand command);
        Task<bool> IsUniqueUsername(string userName);
    }
}
