using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
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
    public class CreateContactCommandTest : TestBase
    {
        

        [Test]
        public async Task ShouldRequiredMinimumField()
        {
            var command = new CreateContactCommand();
            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldRequiredAValidEmail()
        {
            var command = new CreateContactCommand()
            {
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
            var command = new CreateContactCommand()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "AZER",
                Email = "johnsmith@gmail.com"
            };
            FluentActions.Invoking(() => SendAsync(command)).Should().Throw<ValidationException>();
        }
        [Test]
        public async Task ShouldCreateContact()
        {
            var command = new CreateContactCommand()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33606060606",
                Email = "johnsmith@gmail.com"
            };
            var contactId = await SendAsync(command);

            var contact = await FindAsync<Domain.Entities.Contact>(contactId.Data);
            contact.Should().NotBeNull();
            contact.FirstName.Should().Be(command.FirstName);
            contact.LastName.Should().Be(command.LastName);
            contact.PhoneNumber.Should().Be(command.PhoneNumber);
            contact.Email.Should().Be(command.Email);

        }
    }
}
