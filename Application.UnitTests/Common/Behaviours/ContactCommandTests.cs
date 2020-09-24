using Application.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using MediatR;

namespace Application.UnitTests.Common.Behaviours
{
    public class ContactCommandTests
    {
        private Mock<IContactRepository> _contactMock;
        private Mock<ISkillRepository> _skillMock;
        private Mock<IMapper> _mapper;
        private Mock<IMediator> _mediator;
    }
}
