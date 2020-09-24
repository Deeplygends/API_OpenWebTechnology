using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Contacts.Commands;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Application.IntegrationTests.Features.Contact.Command
{
    using static Testing;
    public class UpdateContactCommandTest : TestBase
    {
        [Test]
        public async Task ShouldRequiredMinimumField()
        {
            var command = new UpdateContactCommand();
            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldRequiredAValidEmail()
        {
            var contact = await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "john.smith@gmail.com"
            });

            var command = new UpdateContactCommand()
            {
                Id = contact.Id,
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "eafrae"
            };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldRequiredAValidPhoneNumber()
        {
            var contact = await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "john.smith@gmail.com"
            });

            var command = new UpdateContactCommand()
            {
                Id = contact.Id,
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "AZER",
                Email = "johnsmith@gmail.com"
            };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldUpdateContact()
        {
            var contactAdd = await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "john.smith@gmail.com"
            });

            var command = new UpdateContactCommand()
            {
                Id = contactAdd.Id,
                FirstName = "Jean",
                LastName = "Dupont",
                PhoneNumber = "+33607070707",
                Email = "jeandupont@gmail.com"
            };
            var contactId = await SendAsync(command);

            var contact = contactId.Data;
            contact.Should().NotBeNull();
            contact.FirstName.Should().Be(command.FirstName);
            contact.LastName.Should().Be(command.LastName);
            contact.PhoneNumber.Should().Be(command.PhoneNumber);
            contact.Email.Should().Be(command.Email);

        }
    }
}
