using System;
using Xunit;
using FluentAssertions;
using Domain.Entities;

namespace Domain.UnitTests
{
    public class ContactUnitTest
    {
        private Contact _contact;
        
        public ContactUnitTest()
        {
            _contact = new Contact()
            {
                Id = 1,
                FirstName = "Max",
                LastName = "Dupont",
                Email = "max.dupont@gmail.com",
                PhoneNumber = "+33606060606",
                Address = "6 rue du paradis CH-1201 Geneve"
            };
        }
        #region getter
        [Fact]
        public void GetIdTest()
        {
            _contact.Id.Should().Be(1);
        }
        [Fact]
        public void GetFirstNameTest()
        { 
            _contact.FirstName.Should().BeEquivalentTo("Max");
        }
        [Fact]
        public void GetLastNameTest()
        {
            _contact.LastName.Should().BeEquivalentTo("Dupont");
        }
        [Fact]
        public void GetEmailTest()
        {
            _contact.Email.Should().BeEquivalentTo("max.dupont@gmail.com");
        }
        [Fact]
        public void GetPhoneNumberTest()
        {
            _contact.PhoneNumber.Should().BeEquivalentTo("+33606060606");
        }
        [Fact]
        public void GetAddressTest()
        {
            _contact.Address.Should().BeEquivalentTo("6 rue du paradis CH-1201 Geneve");
        }
        #endregion
        #region setter
        [Fact]
        public void SetIdTest()
        {
            _contact.Id = 2;
            _contact.Id.Should().Be(2);
        }
        [Fact]
        public void SetFirstNameTest()
        {
            _contact.FirstName = "John";
            _contact.FirstName.Should().BeEquivalentTo("John");
        }
        [Fact]
        public void SetLastNameTest()
        {
            _contact.LastName = "Smith";
            _contact.LastName.Should().BeEquivalentTo("Smith");
        }
        [Fact]
        public void SetEmailTest()
        {
            _contact.Email = "john.smith@gmail.com";
            _contact.Email.Should().BeEquivalentTo("john.smith@gmail.com");
        }
        [Fact]
        public void SetPhoneNumberTest()
        {
            _contact.PhoneNumber = "0606060606";
            _contact.PhoneNumber.Should().BeEquivalentTo("0606060606");
        }
        [Fact]
        public void SetAdressTest()
        {
            _contact.Address = "3 rue du lac CH-1201 Genève";
            _contact.Address.Should().BeEquivalentTo("3 rue du lac CH-1201 Genève");
        }
        [Fact]
        public void GetFullNameTest()
        {
            _contact.FullName.Should().BeEquivalentTo("Dupont Max",
                $"the last name is {_contact.LastName} and the last name is {_contact.FirstName}");
        }
        #endregion
    }
}
