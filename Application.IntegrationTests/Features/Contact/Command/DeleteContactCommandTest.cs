using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Contacts.Commands;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Contact.Command
{
    using static Testing;
    public class DeleteContactCommandTest : TestBase
    {
        [Test]
        public async Task ShouldRaiseExceptionDeleteUnexinstantId()
        {
            var command = new DeleteContactCommand();

            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldDeleteContact()
        {
            var contact = await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "john.smith@gmail.com"
            });
            
            var command = new DeleteContactCommand() {Id = contact.Id};

            var response = await SendAsync(command);

            response.Succeeded.Should().BeTrue();
            response.HttpResponse.Should().Be(HttpResponseTypeEnum.Ok);
        }

    }
}
