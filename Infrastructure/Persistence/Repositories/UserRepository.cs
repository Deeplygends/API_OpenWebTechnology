﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Application.Features.User.Command;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private DbSet<User> _users;
      
        public UserRepository(OWTDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();

        }
        public async Task<bool> Authentificate(AuthentificationCommand command)
        {
            var md5 = MD5.Create();
            var password = UTF8Encoding.UTF8.GetBytes(command.Password);
            var hash = md5.ComputeHash(password);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sBuilder.Append(hash[i].ToString("x2"));
            command.Password = sBuilder.ToString();

            var user = await _users.FirstOrDefaultAsync(x => x.Password == command.Password && x.Username == command.UserName);
           if (user != null)
           {
               command.IdContact = user.IdContact;
           }
           return false;
        }

        public async Task<bool> IsUniqueUsername(string userName)
        {
            var isUnique =  await _users.AnyAsync(x => x.Username == userName);
            return !isUnique;
        }

       
    }
}
