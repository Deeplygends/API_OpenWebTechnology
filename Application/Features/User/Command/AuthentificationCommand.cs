using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json;

namespace Application.Features.User.Command
{
    public class AuthentificationCommand : IRequest<Response<AuthentificationCommand>>
    {
        [JsonIgnore]
        public int IdContact { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class AuthentificationCommandHandler : IRequestHandler<AuthentificationCommand, Response<AuthentificationCommand>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthentificationCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Response<AuthentificationCommand>> Handle(AuthentificationCommand command, CancellationToken cancellationToken)
        {
            var auth = await _userRepository.Authentificate(command);
            
            command.Password = "***";
            return new Response<AuthentificationCommand>(command, "", HttpResponseTypeEnum.Ok);
        }
    }
}
