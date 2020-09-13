using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Application.DTOs;
using Application.Features.Contacts.Commands.CreateContact;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ContactDto, Contact>();
            CreateMap<Contact, ContactDto>().ReverseMap();
        }
    }
}
