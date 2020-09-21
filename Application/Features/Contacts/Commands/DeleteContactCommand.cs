using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrapper;
using AutoMapper;
using Domain.Enums;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Contacts.Commands
{
    public class DeleteContactCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Response<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public DeleteContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {  
            _mapper = mapper;
            _contactRepository = contactRepository;
        }

        public async Task<Response<int>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {

            var contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
            {
                var failure = new ValidationFailure("Id", "The id is not a valid id");
                throw new ValidationException(new List<ValidationFailure>() { failure }, HttpResponseTypeEnum.NotFound);
            }
            await _contactRepository.DeleteAsync(contact); 
            return new Response<int>(request.Id, "Ressource Deleted", HttpResponseTypeEnum.Ok);
        }
    }
}
