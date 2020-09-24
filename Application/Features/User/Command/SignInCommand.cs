using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.Results;
using MediatR;
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.Features.User.Command
{
    public class SignInCommand : ContactDto, IRequest<Response<int>>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, Response<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public SignInCommandHandler(IUserRepository userRepository, IMapper mapper, IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Response<int>> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.User>(command);
            if (!await _userRepository.IsUniqueUsername(user.Username))
            {
                var failure = new ValidationFailure("userName", "UserName already exists, choose another one");
                throw new ValidationException(new List<ValidationFailure>(){failure}, HttpResponseTypeEnum.Conflict);
            }
            var contact = _mapper.Map<Contact>(command);
            contact = await _contactRepository.AddAsync(contact);
            
            user.IdContact = contact.Id;
            var md5 = MD5.Create();
            var password = UTF8Encoding.UTF8.GetBytes(user.Password);
            var hash = md5.ComputeHash(password);
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sBuilder.Append(hash[i].ToString("x2"));
            user.Password = sBuilder.ToString();
            user = await _userRepository.AddAsync(user);
            return new Response<int>(1, "Sign In operation is a success", HttpResponseTypeEnum.Ok);
        }
    }
}
