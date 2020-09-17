using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Application.DTOs;
using Application.Features.Contacts.Commands.CreateContact;
using Application.Features.Contacts.Queries.GetAllContacts;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateContactCommand, Contact>();
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
