using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Contacts.Queries;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Application.IntegrationTests.Features.Contact.Queries
{
    using static Testing;
    public class GetContactByIdQueryTest : TestBase
    {
        [Test]
        public async Task ShouldRequiredCorrectId()
        {
            var query = new GetContactByIdQuery();

            FluentActions.Invoking(() => SendAsync(query)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldGetContactById()
        {
            var contact = await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "+33621654684",
                Email = "john.smith@gmail.com"
            });

            var query = new GetContactByIdQuery() {Id = contact.Id};

            var response = await SendAsync(query);

            response.Data.FirstName.Should().Be(contact.FirstName);
            response.Data.LastName.Should().Be(contact.LastName);
            response.Data.Email.Should().Be(contact.Email);
            response.Data.PhoneNumber.Should().Be(contact.PhoneNumber);

        }
    }
}
