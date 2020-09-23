using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Contacts.Queries.GetAllContacts;
using Application.Wrapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NUnit.Framework;

namespace Application.IntegrationTests.Features.Contact.Queries
{
    using static Testing;
    public class GetAllContactsQueryTest : TestBase
    {
        [Test]
        public async Task ShouldReturnAllContacts()
        {
            await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "Jean",
                LastName = "Bon",
                Email = "herta@gmail.com",
                PhoneNumber = "+33606060606",
                Address = "6 rue du cochon 75000 Paris"
            });
            await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "Tom",
                LastName = "De Savoie",
                Email = "emmental@hotmail.com",
                PhoneNumber = "+4122654864862",
                Address = "6 rue de la tartiflette CH-12001 Genève"
            });
            await AddAsync(new Domain.Entities.Contact()
            {
                FirstName = "Sarah",
                LastName = "Pelle",
                Email = "alacatel@swiscom.com",
                PhoneNumber = "+194949115",
                Address = "6 avenue de la 5G 74000 Annecy"
            });
            var query = new GetAllContactsQuery();

            Response<IEnumerable<ContactDto>> response = await SendAsync(query);

            var contacts = response.Data;

            contacts.Should().NotBeNullOrEmpty();
            contacts.Should().HaveCount(3);

        }
    }
}
